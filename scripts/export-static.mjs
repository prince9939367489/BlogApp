import { mkdir, writeFile, copyFile } from "node:fs/promises";
import { resolve, dirname } from "node:path";

const root = resolve(import.meta.dirname, "..");
const origin = process.argv[2] || "http://127.0.0.1:5249";
async function render(path) {
  const response = await fetch(new URL(path, origin));
  if (!response.ok) throw new Error(path + ": HTTP " + response.status);
  return response.text();
}
const home = await render("/");
const slugs = [...new Set([...home.matchAll(/href="\/stories\/([a-z0-9-]+)"/g)].map(match => match[1]))];
if (!slugs.length) throw new Error("No article routes found; refusing an empty export.");
const routes = [["/", "index.html"], ["/Home/About", "about.html"], ["/Home/Privacy", "privacy.html"],
  ...slugs.map(slug => ["/stories/" + slug, "stories/" + slug + ".html"])];
for (const [route, output] of routes) {
  let html = route === "/" ? home : await render(route);
  html = html.replaceAll('href="/Home/About"', 'href="/about.html"')
    .replaceAll('href="/Home/Privacy"', 'href="/privacy.html"')
    .replace(/href="\/stories\/([a-z0-9-]+)"/g, 'href="/stories/$1.html"')
    .replace(/\?v=[^"]+/g, "")
    .replace(/ b-[a-z0-9]+/g, "");
  if (route === "/") html = html.replace('<form class="story-filters"',
    '<noscript><p>Enable JavaScript to search and filter. All four stories are available below.</p><style>.story-filters{display:none}</style></noscript><form class="story-filters"');
  const target = resolve(root, "dist", output);
  await mkdir(dirname(target), { recursive: true });
  await writeFile(target, html.replace(/[ \t]+$/gm, ""));
}
for (const asset of ["css/site.css", "js/site.js", "favicon.ico"]) {
  const target = resolve(root, "dist", asset);
  await mkdir(dirname(target), { recursive: true });
  await copyFile(resolve(root, "BlogApp/wwwroot", asset), target);
}
console.log("Exported " + routes.length + " pages from the MVC application.");
