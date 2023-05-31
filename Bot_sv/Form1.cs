

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Drawing;
using System.Security.Policy;
using ReadBot;

namespace Bot_sv
{

    public partial class Form1 : Form
    {

        public TelegramBotClient botClient;
       // HoiDataBase HoiData;
        //6052997336
        public long chatId = 6052997336; // Mk fix trước 1 cái chat id là tài khuản của mk! 

        int logCounter = 0;

        void AddLog(string msg)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.BeginInvoke((MethodInvoker)delegate ()
                {
                    AddLog(msg);
                });
            }
            else
            {
                logCounter++;
                if (logCounter > 100)
                {
                    txtLog.Clear();
                    logCounter = 0;
                }
                txtLog.AppendText(msg + "\r\n");
            }
            Console.WriteLine(msg);
        }

        
        public Form1()
        {
            InitializeComponent();
           
            
            string token = "6215295516:AAGIz0LLajoweioLsL-67Te9Ai-tPiH_zpo";//duong dan token telegram

           

            botClient = new TelegramBotClient(token);  // Tạo 1  bot 

            CancellationTokenSource cts = new CancellationTokenSource();  // bot này để hủy j đó kiểm soát chương trình
            

            
            ReceiverOptions receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,  //hàm xử lý khi có người chát đến được gọi mỗi khi có cập nhật mới từ telegram API -> nó xử lý và trả về kq  
                pollingErrorHandler: HandlePollingErrorAsync,   // Hàm này sử lý lỗi -> có lỗi là gọi bot này
                receiverOptions: receiverOptions,  // Bot này đc new ở trên kìa tham số cài đặt về việc cập nhật mới
                cancellationToken: cts.Token    // Bot này là hủy cts.Token  -> hủy nó làm j ?
                                                // Tom lại: bắt đầu quá trình nhận cập nhật từ Telegram API bằng cách kích hoạt botClient
                                               
            );

            Task<User> me = botClient.GetMeAsync(); // Được sử dụng để gửi một yêu cầu đến Telegram API để lấy thông tin về bot hiện tại.
            // => Nắm đầu bot  rồi.
            AddLog($"Bot: @{me.Result.Username}");

            //async lập trình bất đồng bộ
            // Trả về đối tượng Task ?? 
            // Vậy là form 
            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                // botClient: bot này mk tạo ở trên rồi: được sử dụng để gửi các yêu cầu tới Telegram API
                // update: chứa thông tin về cập nhật mới nhận được từ Telegram API. Update chứa các thông tin như tin nhắn, sự kiện nhóm, thay đổi trạng thái, v.v.
                //          Vậy là bot botClient yêu cầu -> trả kết quả về bot Update!
                // cancellationToken: bot này nó sử lý khi có lỗi -> Nó k thấy đc gọi nhưng k có nó là lỗi ?<>??? nani
                
                bool ok = false;

                //kdl? biến <=> biến đó có thể nhận NULL

                // Telegram.Bot.Types.Message là một lớp đại diện cho một tin nhắn trong Telegram.
                // Lớp này chứa các thông tin về tin nhắn, bao gồm nội dung, người gửi, người nhận, thời gian gửi, vị trí, hình ảnh, v.v.
                Telegram.Bot.Types.Message message = null; // dấu ? để có thể gán null 

                // update.Message là người dùng nhắn 1 tin nhắn mới tới bot
                if (update.Message != null)  // Nếu tin nếu bot update không phải là null => có cập nhật mới:
                {
                    // message không phải là string -> nó là đối tượng đại diện cho 1 tin nhắn
                    message = update.Message;   // Và tao gán thông tin update vào bot đại diện cho tin nhắn này
                    ok = true;
                }
                // update.EditedMessage là có 1 tin nhắn đã gửi từ trước rồi => song giờ nó click phải chuột sửa tin nhắn -> tao cũng nắm đầu ra xử lý
                if (update.EditedMessage != null)
                {
                    message = update.EditedMessage;
                    ok = true;
                }

                // Nó k chui vào 2 if ở trên <=> !false hoặc message == null => return;
                if (!ok || message == null) return; //thoát ngay

                string messageText = message.Text;
                if (messageText == null) return;  //ko chơi với null

                chatId = message.Chat.Id;  //id của người chát với bot

                AddLog($"{chatId}: {messageText}");  //show lên để xem -> chứ k phải gửi về telegram

                string reply = "";  //đây là text trả lời

                string messLow = messageText.ToLower(); // Có lẽ k cần thiết!




                if (messLow.StartsWith("hi"))
                {
                    reply = "Thích Hi không";

                }
                #region Method
                else if (messLow.StartsWith("gv"))
                {
                    reply = "FeedBack Giáo viên:🥲 Môn học lập trình Windows thầy Đỗ Duy Cốp. Giảng quá xá là HAY!😍😍";
                }
                else if (messLow.StartsWith("/thoitiet"))
                {
                    if (messLow.Length < 11)
                    {
                        reply = "Bạn nên nhập theo cú pháp: Địa điểm + ',' + thời gian";
                    }
                    else
                    {
                        string input = messLow.Substring(10);
                        if (input.Contains(","))
                        {
                            string[] parts = input.Split(',');
                         //   reply = ThoiTiet.GetThoiTiet(parts[0], parts[1]);
                        }
                        else
                        {
                            reply = "Bạn nên nhập theo cú pháp: Địa điểm + ',' + thời gian";
                        }
                    }

                }
                else if (messageText.StartsWith("/timMa"))
                {
                    if (messageText.Length < 10)
                    {
                        reply = "Bạn nên nhập một thứ gì đó để bot có thể hoạt động.";
                    }
                    else
                    {
                        string input2 = messageText.Substring(9);
                        if (input2.Length > 0)
                        {
                            reply = SinhVien.timMaKH(input2);
                        }
                        else
                        {
                            reply = "Bạn nên nhập một thứ gì đó để bot có thể hoạt động.";
                        }
                    }
                }

                #endregion




                else // Nếu k phải là thằng nào đặc biệt thì xuat ra thong tin vua nhap
                {
                    reply = "Toi noi lai: " + messageText;
                }
                
                



                // ----------- KẾT THÚC XỬ LÝ -----------------------------------------------------------------------
                AddLog(reply); //show log to see




                
                Telegram.Bot.Types.Message sentMessage = await botClient.SendTextMessageAsync(
                           // Hàm gửi tin nhắn đi này cần setting như sau:
                           chatId: chatId, // chatId biến này lấy ở trên kia rồi -> lưu id thằng chat với mk để bây giờ trả lời lại nó chứ! chuẩn chưa
                           text: reply,    // rep lại bên telegram thì gán vào thuộc tính text => ở đây là biến reply mk đã xử lý dữ liệu ở trên rồi <>
                           parseMode: ParseMode.Html  // =>  Bro dùng cách đánh dấu văn bản HTML để thể hiện text.
                                                      //parseMode: ParseMode.Markdown => thì nó cũng là 1 cách đánh dấu văn bản nhưng nó k phong phú như html
                      );

               
            }

            // Đây là hàm sử lý lỗi -> có lỗi nó chui vào hàm này
            Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
            {
               
                Console.WriteLine("Looi roi anh ouwi");
                AddLog("----       Lỗi rồi -> K rõ lỗi j  -----------");
                return Task.CompletedTask;
            }
        }

        private void formBot_Load(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}


