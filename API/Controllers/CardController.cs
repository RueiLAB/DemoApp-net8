using API.DTOs.Card;
using API.DTOs.Todo;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CardController : BaseApiController
    {
        private readonly ICardService _cardService;
        private readonly ITodoService _todoService;
        public CardController(ICardService cardService, ITodoService todoService)
        {
            _cardService = cardService;
            _todoService = todoService;
        }

        /// <summary>
        /// 取得所有卡片
        /// </summary>
        /// <returns></returns>
        // GET: api/card
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardDto>>> GetAllCards()
        {
            var cards = await _cardService.GetAllCards();
            return Ok(cards);
        }
        
        /// <summary>
        /// 取得卡片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/card/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CardDto>> GetCardById(int id)
        {
            // 先取得 Card
            var card = await _cardService.GetCardById(id);
            if (card == null) return NotFound();
            
            return Ok(card);
        }
        
        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="createCardDto"></param>
        /// <returns></returns>
        // POST: api/card
        [HttpPost]
        public async Task<ActionResult<CardDto>> CreateCard([FromBody] CreateCardDto createCardDto)
        {
            var card = await _cardService.CreateCard(createCardDto);

            return CreatedAtAction(nameof(GetCardById), new { id = card.Id }, card);
        }

        /// <summary>
        /// 修改卡片
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateCardDto"></param>
        /// <returns></returns>
        // PUT: api/card/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCard(int id, [FromBody] UpdateCardDto updateCardDto)
        {
            var result = await _cardService.UpdateCard(id, updateCardDto);
            if (!result) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/card/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCard(int id)
        {
            var result = await _cardService.DeleteCard(id);
            if (!result) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// 卡片中新增待辦事項
        /// </summary>        
        // POST: api/card/{cardId}/todos
        [HttpPost("{cardId}/todos")]
        public async Task<ActionResult<TodoDto>> AddTodoToCard(int cardId, [FromBody] CreateTodoDto createTodoDto)
        {
            // 確保路由參數與 Request Body 的 CardId 一致
            if (cardId != createTodoDto.CardId)
                return BadRequest("卡片資料不正確");

            // 檢查指定 Card 是否存在
            var card = await _cardService.GetCardById(cardId);            
            if (card == null) return NotFound($"找不到卡片資料");

            var todo = await _todoService.CreateTodo(createTodoDto);
            // 使用 TodoController 的 GET by id 路由名稱作為參考
            return CreatedAtAction("GetTodoById", "Todo", new { id = todo.Id }, todo);
        }
    }
}
