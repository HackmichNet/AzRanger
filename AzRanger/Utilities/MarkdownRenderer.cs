using Markdig;

namespace AzRanger.Utilities
{
    internal static class MarkdownRenderer
    {
        private readonly static MarkdownPipeline _pipeline = new MarkdownPipelineBuilder()
                .UseCitations().UseEmphasisExtras().DisableHtml().Build();


        public static string Render(string data)
        {
            if (data == null)
            {
                return null;
            }
            return Markdown.ToHtml(data, pipeline: MarkdownRenderer._pipeline);
        }
    }


}
