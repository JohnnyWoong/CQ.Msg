using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;

using System;
using System.Text;

namespace Msg.Code
{
    public class Event_GroupMessage : IGroupMessage
    {
        /// <summary>
        /// 收到群消息
        /// Author: Johnny
        /// Time: 2020.08.10
        /// EditTime: 2020.08.13
        /// </summary>
        /// <param name="sender">事件来源</param>
        /// <param name="e">事件参数</param>
        public void GroupMessage(object sender, CQGroupMessageEventArgs e)
        {
            try
            {
                SQLiteHelper sql = new SQLiteHelper(e.CQApi.AppDirectory + "msg.db");
                string sqlstr = string.Format("INSERT INTO 'msg' VALUES (NULL, {0}, {1}, {2}, '{3}', '{4}')", e.CQApi.GetLoginQQ().Id, e.FromGroup.Id, e.FromQQ.Id, e.Message.Text, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sql.ExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                e.CQLog.Error("错误", ex.Message);
            }

            // 设置该属性, 表示阻塞本条消息, 该属性会在方法结束后传递给酷Q
            e.Handler = false;
        }
    }

}
