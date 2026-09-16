(() => {
  "use strict";
  const form = document.querySelector(".story-filters");
  if (!form) return;
  const search = form.querySelector('[name="q"]');
  const category = form.querySelector('[name="category"]');
  const cards = [...document.querySelectorAll(".post-card[data-search]")];
  const count = document.querySelector("#result-count");
  const empty = document.querySelector(".empty-state");
  const params = new URLSearchParams(location.search);
  search.value = params.get("q") || "";
  const selected = params.get("category") || "";
  const option = [...category.options].find(item => item.value.toLowerCase() === selected.toLowerCase());
  if (selected && !option) {
    const unknown = new Option(selected, selected);
    category.add(unknown);
  }
  category.value = option ? option.value : selected;
  function filter(updateUrl = false) {
    const query = search.value.trim().toLowerCase();
    const topic = category.value.toLowerCase();
    let visible = 0;
    cards.forEach(card => {
      card.hidden = !(card.dataset.search.toLowerCase().includes(query) &&
        (!topic || card.dataset.category.toLowerCase() === topic));
      if (!card.hidden) visible++;
    });
    count.textContent = visible + (visible === 1 ? " story found" : " stories found");
    empty.hidden = visible !== 0;
    if (updateUrl) {
      const url = new URL(location.href);
      search.value.trim() ? url.searchParams.set("q", search.value.trim()) : url.searchParams.delete("q");
      topic ? url.searchParams.set("category", category.value) : url.searchParams.delete("category");
      history.replaceState(null, "", url);
    }
  }
  form.addEventListener("submit", event => { event.preventDefault(); filter(true); });
  search.addEventListener("input", () => filter(true));
  category.addEventListener("change", () => filter(true));
  document.querySelectorAll(".reset-link").forEach(link => link.addEventListener("click", event => {
    event.preventDefault(); search.value = ""; category.value = ""; filter(true); search.focus();
  }));
  filter();
})();
