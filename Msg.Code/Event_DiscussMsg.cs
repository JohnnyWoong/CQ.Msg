using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;

using System;
using System.Text;

namespace Msg.Code
{
    public class Event_DiscussMsg : IDiscussMessage
    {
        /// <summary>
        /// 收到讨论组消息
        /// Author: Johnny
        /// Time: 2020.08.10
        /// </summary>
        /// <param name="sender">事件来源</param>
        /// <param name="e">事件参数</param>
        public void DiscussMessage(object sender, CQDiscussMessageEventArgs e)
        {
            try
            {
                SQLiteHelper sql = new SQLiteHelper(e.CQApi.AppDirectory + "msg.db");
                string sqlstr = string.Format("INSERT INTO 'msg' VALUES (NULL, {0}, {1}, {2}, '{3}', '{4}')", e.CQApi.GetLoginQQ().Id, e.FromDiscuss.Id, e.FromQQ.Id, e.Message.Text, DateTime.Now);
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
