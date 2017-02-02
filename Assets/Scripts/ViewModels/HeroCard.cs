using MarkLight;
using MarkLight.Views.UI;

namespace ViewModels
{
    [HideInPresenter]
    public class HeroCard : Region
    {
        public _string Name;
        public _int Level;
        public _string HeroClass;
        public _Sprite HeroSprite;
        public _int ProfitCut;
        public _int Investment;
    }
}