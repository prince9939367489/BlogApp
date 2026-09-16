import assert from "node:assert/strict";
const origin = process.argv[2] || "http://127.0.0.1:5249";
async function get(path) {
  const response = await fetch(new URL(path, origin));
  assert.equal(response.status, 200, path);
  return response.text();
}
const home = await get("/");
assert.match(home, /4 stories found/);
const slugs = [...new Set([...home.matchAll(/href="\/stories\/([a-z0-9-]+)"/g)].map(match => match[1]))];
assert.equal(slugs.length, 4);
for (const slug of slugs) {
  const html = await get("/stories/" + slug);
  assert.match(html, /class="article-body"/);
  assert.equal((html.match(/<section><h2>/g) || []).length, 4);
}
assert.equal((await fetch(origin + "/stories/missing-story")).status, 404);
assert.match(await get("/?category=Design"), /1 story found/);
assert.match(await get("/?category=development"), /2 stories found/);
assert.match(await get("/?q=quiet&category=Design"), /1 story found/);
assert.match(await get("/?q=quiet&category=Learning"), /0 stories found/);
assert.match(await get("/?q=%20quiet%20"), /1 story found/);
const escaped = await get("/?q=%3Cscript%3Ealert(1)%3C%2Fscript%3E");
assert.ok(!escaped.includes("<script>alert(1)</script>"));
assert.match(escaped, /0 stories found/);
await get("/Home/About");
await get("/Home/Privacy");
await get("/css/site.css");
await get("/js/site.js");
console.log("Passed: four articles, 404, combined filters, whitespace, HTML escaping, supporting pages and assets.");
