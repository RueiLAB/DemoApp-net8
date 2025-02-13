using System;

namespace API.Entities;

public class Todo
{
    public int Id { get; set; }
        
        /// <summary>
        /// 待辦事項標題
        /// </summary>
        public required string Title { get; set; }
        
        /// <summary>
        /// 是否已完成
        /// </summary>
        public bool IsCompleted { get; set; }
        
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// 完成時間，若尚未完成則為 null
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// 關聯卡片Id
        /// </summary>
        public int? CardId { get; set; }
}
