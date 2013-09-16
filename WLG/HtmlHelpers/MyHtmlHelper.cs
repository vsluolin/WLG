using System;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Linq.Expressions;

namespace WLG.HtmlHelpers
{
    public static class MyHtmlHelper
    {
        /// <summary>
        /// 自定义一个@html.Image()
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="src">src属性</param>
        /// <param name="alt">alt属性</param>
        /// <returns></returns>
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string alt)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", alt);
            builder.ToString(TagRenderMode.SelfClosing);
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper, string name, string codeCategory, RepeatDirection repeatDirection = RepeatDirection.Horizontal)
        {
            var codes = CodeManager.GetCodes(codeCategory);
            return ListControlUtil.GenerateHtml(name, codes, repeatDirection, "radio", null);
        }
        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string codeCategory, RepeatDirection repeatDirection = RepeatDirection.Horizontal)
        {
            var codes = CodeManager.GetCodes(codeCategory);
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
            string name = ExpressionHelper.GetExpressionText(expression);
            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            return ListControlUtil.GenerateHtml(fullHtmlFieldName, codes, repeatDirection, "radio", metadata.Model);
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, string codeCategory, RepeatDirection repeatDirection = RepeatDirection.Horizontal)
        {
            var codes = CodeManager.GetCodes(codeCategory);
            return ListControlUtil.GenerateHtml(name, codes, repeatDirection, "checkbox", null);
        }
        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string codeCategory, RepeatDirection repeatDirection = RepeatDirection.Horizontal)
        {
            var codes = CodeManager.GetCodes(codeCategory);
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
            string name = ExpressionHelper.GetExpressionText(expression);
            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            return ListControlUtil.GenerateHtml(fullHtmlFieldName, codes, repeatDirection, "checkbox", metadata.Model);
        }



    }
}