using SyngentaWeigherQC.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmStartup : Form
  {
    
    private (string Content, string Author)[] message = new (string, string)[]
    {
        ("Những gì không giết chết được ta sẽ khiến ta mạnh mẽ hơn.", "Angelina Jolie"),
        ("Cách tốt nhất để khởi đầu là ngừng nói và bắt đầu làm. ", "Walt Disney"),
        ("Người bi quan nhìn khó khăn ra cơ hội. Người lạc quan nhìn thấy cơ hội trong khó khăn. ", "Winston Churchill"),
        ("Đừng để ngày hôm qua chiếm hữu quá nhiều thời gian của ngày hôm nay.", "Will Rogers"),
        ("Người ta học được nhiều thất bại hơn là thành công. Đừng để thất bại cản bước bản, bởi thất bại sẽ làm nên con người bạn.", "Khuyết danh"),
        ("Vấn đề không nằm ở chỗ bạn có gục ngã hay không mà là bạn có đứng dậy sau đó không.", "Vince Lombardi"),
        ("Nếu bạn thực sự yêu thích điều mình làm, bạn sẽ không cần ai khác thúc đẩy.", "Steve Jobs"),
        ("Ai đủ điên rồ để nghĩ mình có thể thay đổi thế giới, chính họ sẽ làm được điều này.", "Rob Siltanent"),
        ("Thất bại sẽ mãi mãi không thể thắng được tôi, nếu khao khát đạt được thành công đủ mạnh mẽ.", "Og Mandino"),
        ("Bạn có thể thất bại liên tục, nhưng không được để bản thân bị đánh bại.", "Maya Angelou"),
        ("Hãy tưởng tượng bạn đang có một cuộc sống hoàn hảo ở mọi khía cạnh. Vậy nó sẽ trông như thế nào?", "Brian Tracy"),
        ("Khi gặp khó khăn thay vì than thở, hãy đứng dậy tìm đi cách giải quyết.", "Khuyết danh"),
        ("Chỉ cần có niềm tin, bạn sẽ có hy vọng, và tìm thấy con đường để giải quyết bế tắc của mình.", "Khuyết danh"),
        ("Cơ hội đến với tất cả mọi người, nhưng để nắm bắt và đạt được thành công thì phụ thuộc vào cách mỗi người trải nghiệm nó.", "Khuyết danh"),
        ("Vinh quang là cách chúng ta đứng dậy sau những vấp ngã.", ""),
        ("Khi mọi thứ dường như đang chống lại bạn. Hãy nhớ rằng, máy bay đang chống lại gió để cất cánh, chứ không phải thuận theo nó.", "Khuyết danh"),
        ("Cố gắng và hối hận, cái nào đau đớn hơn?", "Khuyết danh"),
        ("Tại sao không khiến mình trở nên ưu tú hơn để thu hút người khác mà lại phải chạy theo đuôi người ta.", "Khuyết danh"),
        ("Ba năm gặm bánh mì dẫu sao cũng hơn 30 năm gặm bánh mì.", "Khuyết danh"),
        ("Dù bạn nghĩ rằng mình có thể hay không thể, bạn đều đúng.", "Henry Ford"),
        ("Không gì là không thể với những người luôn biết cố gắng.", "Khuyết danh "),
        ("Đủ nắng hoa sẽ nở. Đủ gió chong chóng sẽ quay. Đủ yêu thương hạnh phúc sẽ đong đầy. ", "Khuyết danh "),
        ("Hạnh phúc không thuộc vào bạn là ai, bạn làm gì mà tùy thuộc vào bạn đang nghĩ gì.", "Khuyết danh "),
        ("Nhìn xuống để thấy cuộc đời này ta còn may mắn hơn bao nhiêu người và ngước lên để thấy cuộc đời này ta cần cố gắng nhiều hơn nữa.", "Khuyết danh "),
        ("Sống tích cực, nghĩ tích cực. Nói ít làm nhiều, có thể đứng thì không ngồi, có thể đi thì đừng bao giờ đứng yên một chỗ.", "Khuyết danh "),
        ("Đừng so sánh bản thân với bất kỳ một ai khác bởi mỗi người trong chúng ta là một người đặc biệt.", "Khuyết danh "),
        ("Cho dù không ai thương bạn, bạn cũng cần phải yêu thương chính mình.", "Khuyết danh "),
        ("Nghị lực và kiên trì sẽ chiến thắng tất cả.", "Benjamin Franklin"),
        ("Hãy dám đối mặt và vượt qua những trở ngại, bạn sẽ nhận ra chúng không thực sự đáng ngại như bạn nghĩ.", "Khuyết danh "),
        ("Người có lòng tin vào chính mình sẽ có được lòng tin của người khác.", "Ngạn ngữ người Do Thái"),
        ("Sáng tạo là khi bạn cho phép trí thông minh được chơi đùa. ", "Albert EInstein "),
        ("Nếu bạn nghĩ bạn đã biết đủ nhiều, bạn sinh ra chỉ để làm người bình thường.", "Donald Trump"),
        ("Khi bạn đang 25 tuổi, hãy cứ sai lầm đi, đừng lo lắng gì cả. Ngã thì đứng dậy, thất bại thì lại vùng lên thôi.", "Jack Ma"),
        ("Hôm nay khó khăn, ngày mai còn tồi tệ hơn nhưng ngày kia mọi thứ sẽ tuyệt vời.", "Jack Ma"),
        ("Khi một người được ngồi trong bóng mát ngày hôm nay thì đó là vì ai đó đã trồng 1 cái cây từ trước đó rất lâu.", "Warren Edward Buffett"),
        ("Hãy giao du với người tốt hơn bạn. Hãy chọn những người bạn cách hành xử tốt hơn bạn. Như vậy bạn sẽ tốt hơn. ", "Warren Edward Buffett"),
        ("Hãy ngó lơ mọi hận thù và những lời chỉ trích. Sống vì những gì bạn tạo ra và bảo vệ nó đến chết.", "Lady Gaga"),
        ("Luôn đấu tranh và nỗ lực nhiều hơn nữa cho những gì bạn tin tưởng, bạn sẽ ngạc nhiên vì nhận ra bản thân mạnh mẽ hơn bạn nghĩ đấy!", "Lady Gaga"),
        ("Trang sức là kiến thức, sắc đẹp là vũ khí và khiêm tốn làm nên sự tao nhã.", "Coco Chanel"),
        ("Không có thành công nào là dễ dàng và không có ước mơ nào là xa vời nếu bạn biết cố gắng.", "Khuyết danh"),
    };

    public delegate void SendCloseStartup();
    public event SendCloseStartup OnSendCloseStartup;


    public FrmStartup()
    {
      InitializeComponent();
      this.lbVersion.Text = $"Version: {AppCore.Ins.Version}";
      this.FormClosing += FrmStartup_FormClosing;
    }

    private void FrmStartup_FormClosing(object sender, FormClosingEventArgs e)
    {
      OnSendCloseStartup?.Invoke();
    }
    


    private System.Timers.Timer timerCheckDelay = new System.Timers.Timer();
    private int max_cycle = 5;
    private int current_cycle = 0;

    private void FrmStartup_Load(object sender, EventArgs e)
    {
      Random random = new Random();
      int indexMessage = random.Next(message.Length);
      LoadUIMessage(message[indexMessage].Content, message[indexMessage].Author);

      timerCheckDelay.Interval = 100;
      timerCheckDelay.Elapsed += TimerCheckDelay_Elapsed;
      timerCheckDelay.Start();
    }
    private void TimerCheckDelay_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      timerCheckDelay.Stop();
      if (current_cycle < max_cycle)
      {
        current_cycle++;
        LoadUIProcessing((current_cycle * 100) / max_cycle);
      }
      else
      {
        CloseFrm();
        FormMain.Instance.ShowMainForm();
        return;
      }
      timerCheckDelay.Start();
    }

    private void CloseFrm()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          CloseFrm();
        }));
        return;
      }
      this.Close();
    }

    private void LoadUIProcessing(int value)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadUIProcessing(value);
        }));
        return;
      }
      value = (value > 100) ? 100 : value;
      progressBarOpenning.Value = value;
    }
    private void LoadUIMessage(string content, string author)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadUIMessage(content, author);
        }));
        return;
      }

      this.lbMessage.Text = $"\"{content}\"";
      this.lbAuthor.Text = $"- {author} -"; ;
    }
  }
}
