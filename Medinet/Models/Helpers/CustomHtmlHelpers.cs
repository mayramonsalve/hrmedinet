using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medinet.Models.Helpers
{
    public static class CustomHtmlHelpers
    {
        public static string ImageActionLink(   this HtmlHelper html, string imgSrc, string actionName, 
                                                string controllerName, object routeValues, string altText, 
                                                string titleText, string width)
        {
            UrlHelper urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            string imgUrl = urlHelper.Content(imgSrc);
            TagBuilder imgTagBuilder = new TagBuilder("img");
            imgTagBuilder.MergeAttribute("src", imgUrl);
            imgTagBuilder.MergeAttribute("alt", altText);
            imgTagBuilder.MergeAttribute("title", titleText);
            imgTagBuilder.MergeAttribute("width", width);
            string img = imgTagBuilder.ToString(TagRenderMode.SelfClosing);
            string url = urlHelper.Action(actionName, controllerName, routeValues);

            TagBuilder tagBuilder = new TagBuilder("a")
            {
                InnerHtml = img
            };

            tagBuilder.MergeAttribute("href", url);
            return tagBuilder.ToString(TagRenderMode.Normal);
        }

        public static string Label(this HtmlHelper helper, string target, string text)
        {
            return String.Format("<label for='{0}'>{1}</label>", target, text);
        }
    }
}