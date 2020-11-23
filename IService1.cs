using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
    

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);


        // TODO: Add your service operations here
        [ServiceContract]
        public interface IService1
        {
            [OperationContract]
            string pemesanan(string IDPemesanan, string NamaCostumer, String NoTelpon, int JumlahPemesanan, String IDLokasi); //method //proses input data
            [OperationContract]
            string editPemesanan(string IDPemesanan, string NamaCostumer);
            [OperationContract]
            string deletePemesanan(string IDPemesanan);
            [OperationContract]
            List<CekLokasi> RewiewLokasi(); //nampilin data yg ada di database (select ALl) //menampilkan isi dari yang ada contract
            [OperationContract]
            List<DetailLokasi> DetailLokasi(); //menampilkan detail lokasi
            [OperationContract]
            List<Pemesanan> Pemesanan();


        }

        
    }


    [DataContract]
    public class CekLokasi //daftar lokasi nongkrong
    {
        [DataMember]
        public string IDLokasi { get; set; }  //variabel dari public class
        [DataMember]
        public string NamaLokasi { get; set; }
        [DataMember]
        public string DeskripsiSingkat { get; set; }
    }

    [DataContract]
    public class DetailLokasi  //menampilkan detail lokasi
    {
        [DataMember]
        public string IDLokasi { get; set; }
        [DataMember]
        public string NamaLokasi { get; set; }
        [DataMember]
        public string DeskripsiFull { get; set; }
        [DataMember]
        public int Kuota { get; set; }
    }

    [DataContract]
    public class Pemesanan  // Create
    {
        [DataMember]
        public string IDPemesanan { get; set; }
        [DataMember]
        public string NamaCostumer { get; set; }  //method
        [DataMember]
        public string NoTelpon { get; set; }
        [DataMember]
        public int JumlahPemesanan { get; set; }
        [DataMember]
        public string IDLokasi { get; set; }
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "ServiceReservasi.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    

}
