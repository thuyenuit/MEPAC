using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages.Html;

namespace SMS.Share.Shares
{
    public static class HtmlHelperExtension
    {
        /// <summary>
        /// minify js
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="markup"></param>
        /// <returns></returns>
        /*public static MvcHtmlString JsMinify(
           this System.Web.Mvc.HtmlHelper helper, Func<object, object> markup)
        {
            string notMinifiedJs =
             (markup.DynamicInvoke(helper.ViewContext) ?? "").ToString();

            var minifier = new Minifier();
            var minifiedJs = minifier.MinifyJavaScript(notMinifiedJs, new CodeSettings
            {
                EvalTreatment = EvalTreatment.MakeImmediateSafe,
                PreserveImportantComments = false
            });
            return new MvcHtmlString(minifiedJs);
        }*/

        public static MvcHtmlString SMSTextbox(this System.Web.Mvc.HtmlHelper html, string label, string text, object value,
            IDictionary<string, object> htmlAttributes, bool required = false, bool readOnly = false, bool disabled = false)
        {
            if (htmlAttributes.ContainsKey("class"))
            {
                string strClass = Utils.GetString(htmlAttributes, "class");
                if (!"form-control".Contains(strClass))
                {
                    strClass += " form-control";
                    htmlAttributes["class"] = strClass;
                }
            }
            else
            {
                htmlAttributes["class"] = "form-control";
            }

            if (readOnly)
            {
                htmlAttributes["readonly"] = "readonly";
            }
            if (disabled)
            {
                htmlAttributes["disabled"] = "disabled";
            }

            string strHtml = string.Empty;
            if (required)
            {
                strHtml = "<div class=\"form-group row " + text + "\">"
                            + "<div class=\"editor-label col-form-label text-right col-md-4\" style=\"padding-right: 0px;\">" + html.Label(label) + "<span style=\"color:red\"> *</span></div>"
                            + "<div class=\"editor-field col-md-8\">" + html.TextBox(text, value, htmlAttributes) + "</div>"
                        + "</div>";

                if (string.IsNullOrEmpty(label))
                {
                    strHtml = "<div class=\"form-group row " + text + "\">"
                                + "<div class=\"editor-label col-form-label text-right col-md-4\"></div>"
                                + "<div class=\"editor-field col-md-8\">" + html.TextBox(text, value, htmlAttributes) + "</div>"
                            + "</div>";
                }
                else
                {
                    strHtml = "<div class=\"form-group row " + text + "\">"
                                + "<div class=\"editor-label col-form-label text-right col-md-4\" style=\"padding-right: 0px;\">" + html.Label(label) + "<span style=\"color:red\"> *</span></div>"
                                + "<div class=\"editor-field col-md-8\">" + html.TextBox(text, value, htmlAttributes) + "</div>"
                            + "</div>";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(label))
                {
                    strHtml = "<div class=\"form-group row " + text + "\">"
                                + "<div class=\"editor-label col-form-label text-right col-md-4\"></div>"
                                + "<div class=\"editor-field col-md-8\">" + html.TextBox(text, value, htmlAttributes) + "</div>"
                            + "</div>";
                }
                else
                {
                    strHtml = "<div class=\"form-group row " + text + "\">"
                                + "<div class=\"editor-label col-form-label text-right col-md-4\"  style=\"padding-right: 0px;\">" + html.Label(label) + "</div>"
                                + "<div class=\"editor-field col-md-8\">" + html.TextBox(text, value, htmlAttributes) + "</div>"
                            + "</div>";
                }
            }

            return new MvcHtmlString(strHtml);
        }

        public static MvcHtmlString SMSTextboxFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression,
            string label, string text, IDictionary<string, object> htmlAttributes, bool required = false, bool readOnly = false, bool disabled = false)
        {
            if (htmlAttributes.ContainsKey("class"))
            {
                string strClass = Utils.GetString(htmlAttributes, "class");
                if (!"form-control".Contains(strClass))
                {
                    strClass += " form-control";
                    htmlAttributes["class"] = strClass;
                }
            }
            else
            {
                htmlAttributes["class"] = "form-control";
            }

            if (readOnly)
            {
                htmlAttributes["readonly"] = "readonly";
            }
            if (disabled)
            {
                htmlAttributes["disabled"] = "disabled";
            }

            string strHtml = string.Empty;
            if (required)
            {
                strHtml = "<div class=\"form-group row " + text + "\">"
                            + "<div class=\"editor-label col-form-label text-right col-md-4\" style=\"padding-right: 0px;\">" + html.Label(label) + "<span style=\"color:red\"> *</span></div>"
                            + "<div class=\"editor-field col-md-8\">" + html.TextBoxFor<TModel, TProperty>(expression, htmlAttributes) + "</div>"
                        + "</div>";

                if (string.IsNullOrEmpty(label))
                {
                    strHtml = "<div class=\"form-group row " + text + "\">"
                                + "<div class=\"editor-label col-form-label text-right col-md-4\"></div>"
                                + "<div class=\"editor-field col-md-8\">" + html.TextBoxFor<TModel, TProperty>(expression, htmlAttributes) + "</div>"
                            + "</div>";
                }
                else
                {
                    strHtml = "<div class=\"form-group row " + text + "\">"
                                + "<div class=\"editor-label col-form-label text-right col-md-4\" style=\"padding-right: 0px;\">" + html.Label(label) + "<span style=\"color:red\"> *</span></div>"
                                + "<div class=\"editor-field col-md-8\">" + html.TextBoxFor<TModel, TProperty>(expression, htmlAttributes) + "</div>"
                            + "</div>";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(label))
                {
                    strHtml = "<div class=\"form-group row " + text + "\">"
                                + "<div class=\"editor-label col-form-label text-right col-md-4\"></div>"
                                + "<div class=\"editor-field col-md-8\">" + html.TextBoxFor<TModel, TProperty>(expression, htmlAttributes) + "</div>"
                            + "</div>";
                }
                else
                {
                    strHtml = "<div class=\"form-group row " + text + "\">"
                                + "<div class=\"editor-label col-form-label text-right col-md-4\"  style=\"padding-right: 0px;\">" + html.Label(label) + "</div>"
                                + "<div class=\"editor-field col-md-8\">" + html.TextBoxFor<TModel, TProperty>(expression, htmlAttributes) + "</div>"
                            + "</div>";
                }
            }

            return new MvcHtmlString(strHtml);
        }
    }
}
