using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Collections.ObjectModel;
using System.Web.UI.WebControls;
using System.Text;

namespace WLG.HtmlHelpers
{
    public static class ListControlUtil
    {
        public static MvcHtmlString GenerateHtml(string name, Collection<CodeDescription> codes, RepeatDirection repeatDirection, string type, object stateValue)
        {
            TagBuilder table = new TagBuilder("table");
            int i = 0;
            bool isCheckBox = type == "checkbox";
            if (repeatDirection == RepeatDirection.Horizontal)
            {
                TagBuilder tr = new TagBuilder("tr");
                foreach (var code in codes)
                {
                    i++;
                    string id = string.Format("{0}_{1}", name, i);
                    TagBuilder td = new TagBuilder("td");
                    bool isChecked = false;
                    if (isCheckBox)
                    {
                        IEnumerable<string> currentValues = stateValue as IEnumerable<string>;
                        isChecked = (null != currentValues && currentValues.Contains(code.Code));
                    }
                    else
                    {
                        string currentValue = stateValue as string;
                        isChecked = (null != currentValue && code.Code == currentValue);
                    }

                    td.InnerHtml = GenerateRadioHtml(name, id, code.Description, code.Code, isChecked, type);
                    tr.InnerHtml += td.ToString();
                }
                table.InnerHtml = tr.ToString();
            }
            else
            {
                foreach (var code in codes)
                {
                    TagBuilder tr = new TagBuilder("tr");
                    i++;
                    string id = string.Format("{0}_{1}", name, i);
                    TagBuilder td = new TagBuilder("td");
                    bool isChecked = false;
                    if (isCheckBox)
                    {
                        IEnumerable<string> currentValues = stateValue as IEnumerable<string>;
                        isChecked = (null != currentValues && currentValues.Contains(code.Code));
                    }
                    else
                    {
                        string currentValue = stateValue as string;
                        isChecked = (null != currentValue && code.Code == currentValue);
                    }

                    td.InnerHtml = GenerateRadioHtml(name, id, code.Description, code.Code, isChecked, type);
                    tr.InnerHtml = td.ToString();
                    table.InnerHtml += tr.ToString();
                }
            }
            return new MvcHtmlString(table.ToString());
        }

        private static string GenerateRadioHtml(string name, string id, string labelText, string value, bool isChecked, string type)
        {
            StringBuilder sb = new StringBuilder();
            TagBuilder label = new TagBuilder("label");
            label.MergeAttribute("for", id);
            label.SetInnerText(labelText);
            TagBuilder input = new TagBuilder("input");
            input.GenerateId(id);
            input.MergeAttribute("name", name);
            input.MergeAttribute("type", type);
            input.MergeAttribute("value", value);
            if (isChecked)
            {
                input.MergeAttribute("checked", "checked");
            }
            sb.AppendLine(input.ToString());
            sb.AppendLine(label.ToString());
            return sb.ToString();
        }

    }

    public class CodeDescription
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public CodeDescription(string code, string description, string category)
        {
            this.Code = code;
            this.Description = description;
            this.Category = category;
        }
    }

    public static class CodeManager
    {
        private static CodeDescription[] codes = new CodeDescription[]
       {
          new CodeDescription("M","Male","Gender"),
          new CodeDescription("F","Female","Gender"),
          new CodeDescription("S","Single","MaritalStatus"),
          new CodeDescription("M","Married","MaritalStatus"),
          new CodeDescription("CN","China","Country"),
          new CodeDescription("US","Unite States","Country"),
          new CodeDescription("UK","Britain","Country"),
          new CodeDescription("SG","Singapore","Country")
      };

        public static Collection<CodeDescription> GetCodes(string category)
        {
            Collection<CodeDescription> codeCollection = new Collection<CodeDescription>();
            foreach (var code in codes.Where(code => code.Category == category))
            {
                codeCollection.Add(code);
            }
            return codeCollection;
        }
    }
}