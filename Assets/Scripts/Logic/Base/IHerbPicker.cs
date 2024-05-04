using System.Collections.Generic;
public interface IHerbPicker
{
    public void Init();
    public IList<HerbRawItem> GoPicking();

    public bool CanPick();

    public bool IsFreeThisTime();
}
