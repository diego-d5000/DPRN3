using System.Data;

namespace DPRN3_DIPL
{
    interface ActiveRecord 
    {
        void Save();
        void Delete();
        void Fetch(int id);
    }
}