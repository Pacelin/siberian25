using Siberian25.UI.Common;
using UnityEngine;

namespace Siberian25.UI.ButtonHandlers.Social
{
    public class TwitterButtonHandler : ButtonHandlerBase
    {
        protected override void OnClick() => Application.OpenURL("https://x.com/thespinningsofa");
        protected override void OnHover() { }
    }
}