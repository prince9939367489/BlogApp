namespace BlogApp.Models;

public sealed record ArticleSection(string Heading, string Body);
public sealed record BlogPost(string Slug, string Title, string Category, string Summary, ArticleSection[] Sections)
{
    public int ReadingMinutes => Math.Max(1, (int)Math.Ceiling(
        (Summary + " " + string.Join(" ", Sections.Select(s => s.Heading + " " + s.Body)))
        .Split(' ', StringSplitOptions.RemoveEmptyEntries).Length / 200.0));
}

public static class PostCatalog
{
    public static IReadOnlyList<BlogPost> All { get; } = Array.AsReadOnly(new BlogPost[]
    {
        new("start-small", "Build the smallest version that can teach you something", "Development", "Good side projects begin with one useful question. A compact first release makes the answer visible.", new ArticleSection[]
        {
            new("Start with a question", "Before choosing a framework, write down the uncertainty your project should resolve. Can a reader find an interesting story? Can a learner return to the next lesson? A concrete question gives you a smaller, more useful first release than a long list of features."),
            new("Build one complete journey", "Choose a beginning, a useful action, and a clear result. For a blog, that might mean browsing a list, opening an article, and returning to the collection. Finish that journey before adding accounts, dashboards, or comments. Each extra screen brings states that need design, implementation, and testing."),
            new("Make the result observable", "Try the journey on a narrow screen and with a keyboard. Ask someone to complete it without your instructions. Notice where they hesitate, where text is unclear, and where a link leads nowhere. These observations tell you more than another week of adding features."),
            new("Let evidence choose the next step", "Keep a short record of what worked and what did not. Select one improvement that removes a real obstacle, then ship again. A small release is useful when it teaches you something specific and leaves the next version easier to build."),
        }),
        new("quiet-focus", "Designing for quiet focus", "Design", "Clarity often comes from deciding what the page can leave out, and giving the remaining details room to breathe.", new ArticleSection[]
        {
            new("Give the page a purpose", "A reading page should help someone read. That sounds obvious until navigation, badges, promotions, and controls all compete with the article. Start by deciding what matters most, then make the visual hierarchy support that decision."),
            new("Use space as structure", "A comfortable line length and consistent spacing help readers follow an argument. Group related details, separate new sections, and keep headings close to the paragraphs they introduce. Empty space does useful work when it explains relationships."),
            new("Keep interactions recognizable", "Links should look like links, buttons should describe an action, and focus should remain visible for keyboard users. An elegant interface still needs clear feedback. A search that finds nothing should explain the result and offer a way to start again."),
            new("Review at the edges", "Test long titles, small screens, enlarged text, and reduced-motion preferences. These conditions reveal whether the design is flexible. Quiet focus comes from predictable behavior as much as from a restrained color palette."),
        }),
        new("clear-boundaries", "The useful discipline of clear boundaries", "Development", "Small components, explicit routes, and honest project scope make software easier to explain and extend.", new ArticleSection[]
        {
            new("Separate content from presentation", "When article text lives inside a page layout, even a small content change can disturb the design. A simple content model gives titles, summaries, categories, and sections a clear home. Views can then concentrate on how those details are presented."),
            new("Make routes a promise", "A link should lead to a stable destination. Give each article a readable slug and handle unknown slugs with a real not-found response. Readers can bookmark a story, share its address, and return without depending on the state of a previous screen."),
            new("Keep the first abstraction small", "You do not need a complex service layer for four sample articles. Start with a catalog that describes the current needs clearly. Introduce storage and editing when the project needs them, while keeping the boundary between content and rendering intact."),
            new("Document what exists", "A project is easier to evaluate when its documentation distinguishes working behavior from future plans. Explain how to run it, what to try, and what is still missing. Honest scope helps the next contributor make a useful change."),
        }),
        new("after-you-ship", "What changes after you ship", "Learning", "A finished first version replaces guesses with feedback and turns a private exercise into a public record of growth.", new ArticleSection[]
        {
            new("Finish a small loop", "A project becomes easier to understand once another person can use it. Publishing a complete, modest experience forces decisions about wording, navigation, errors, and setup that tutorials often leave open."),
            new("Invite specific feedback", "Ask someone to find a story about a particular topic or explain what they think a control will do. Specific tasks produce concrete observations. A broad question about whether they like the site is less likely to reveal a problem you can fix."),
            new("Record the surprises", "Keep notes on assumptions that did not survive use. Perhaps an article title is too vague, a mobile control is hard to reach, or the setup instructions miss a prerequisite. These are opportunities to improve the product and your development habits."),
            new("Keep the next release manageable", "Choose the smallest improvement that answers the strongest feedback. Verify the original journey still works, update the documentation, and publish the change. Repeated, understandable improvements build confidence more reliably than an unfinished rewrite."),
        }),
    });
}

