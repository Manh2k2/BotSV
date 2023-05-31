using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_sv
{
    public class ListSinhVien
    {
        private static ListSinhVien instance;
        List<SinhVien> listMaSV;
        public List<SinhVien> ListMaSV { get => listMaSV; set => listMaSV = value; }

        public static ListSinhVien Instance
        {
            get
            {
                if (instance == null)
                    instance = new ListSinhVien();
                return instance;
            }
            set => instance = value;
        }
        private ListSinhVien()
        {
            listMaSV = new List<SinhVien>();
            listMaSV.Add(new SinhVien("SV01","Ha Van Khanh","9"));
            listMaSV.Add(new SinhVien("SV02", "Nguyen Duc Manh", "6"));
            listMaSV.Add(new SinhVien("SV03", "Do Tuan Anh", "7"));
            listMaSV.Add(new SinhVien("SV04", "Ha Trung Hieu", "7"));


        }
    }
}