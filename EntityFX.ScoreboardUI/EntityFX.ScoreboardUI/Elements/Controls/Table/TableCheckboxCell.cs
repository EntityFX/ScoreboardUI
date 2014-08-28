namespace EntityFX.ScoreboardUI.Elements.Controls.Table
{
    public class TableCheckboxCell : TableCellBase
    {
        public TableCheckboxCell()
        {
            RootControl = new Checkbox()
            {
                Parent = this
            };
        }
    }
}