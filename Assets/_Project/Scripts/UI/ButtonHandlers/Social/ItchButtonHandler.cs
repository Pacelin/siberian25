using Siberian25.UI.Common;
using UnityEngine;

namespace Siberian25.UI.ButtonHandlers.Social
{
    public class ItchButtonHandler : ButtonHandlerBase
    {
        protected override void OnClick() => Application.OpenURL("https://thespinningsofa.itch.io");
        protected override void OnHover() { }
    }
}