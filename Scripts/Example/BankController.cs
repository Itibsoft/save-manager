using System.Runtime.Serialization;

namespace SticksCandy.Services.Save.Example
{
    public class BankController : DataController<BankData>
    {
        protected override string Name => "bank_controller";
        
        public BankController(IStorage storage) : base(storage)
        {
        }

        private void Test()
        {
            FastPrefs.SetInt("save_test", 1);

            var temp = FastPrefs.GetInt("save_test");

            if (FastPrefs.HasKeyInt("save_test"))
            {
              
            }
            
            FastPrefs.DeleteAll();
            
            FastPrefs.Save();
        }
        
        protected override BankData CreateDefaultData()
        {
            return new BankData { Count = 10 };
        }
    }

    [DataContract]
    public class BankData
    {
        [DataMember] public int Count;
    }
}