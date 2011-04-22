namespace LeDoyen.AtContract.Business.Entity
{
    public partial class Contractor
    {
        public string FullName
        {
            get { return string.Format("{0} {1} {2}", LastName, FirstName, MiddleName); }
        }
    }
}