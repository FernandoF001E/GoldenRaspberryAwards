using Entity.Data;
using Infrastruture.Context;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/awards")]
    [ApiController]
    public class GoldenRaspberryAwardsController : ControllerBase
    {
        private readonly AwardsContext _context;

        public GoldenRaspberryAwardsController(AwardsContext context)
        {
            _context = context;
        }

        [HttpGet("intervals")]
        public IActionResult GetIntervals()
        {
            try
            {
                var winners = _context.Awards.Where(a => a.Winner == "yes").ToList();

                var producerIntervals = new List<(string Producer, int Interval, int PreviousWin, int FollowingWin)>();

                foreach (var producer in winners.SelectMany(a => a.Producers.Split(",").Distinct()))
                {
                    var years = winners
                        .Where(a => a.Producers.Contains(producer))
                        .Select(a => a.Year)
                        .OrderBy(y => y)
                        .ToList();

                    for (int i = 1; i < years.Count; i++)
                    {
                        producerIntervals.Add((
                            Producer: producer,
                            Interval: years[i] - years[i - 1],
                            PreviousWin: years[i - 1],
                            FollowingWin: years[i]
                        ));
                    }
                }

                var minInterval = producerIntervals.Min(i => i.Interval);
                var maxInterval = producerIntervals.Max(i => i.Interval);

                var minResults = producerIntervals
                    .Where(i => i.Interval == minInterval)
                    .Select(i => new IntervalItem
                    {
                        Producer = i.Producer,
                        Interval = i.Interval,
                        PreviousWin = i.PreviousWin,
                        FollowingWin = i.FollowingWin
                    }).ToList();

                var maxResults = producerIntervals
                    .Where(i => i.Interval == maxInterval)
                    .Select(i => new IntervalItem
                    {
                        Producer = i.Producer,
                        Interval = i.Interval,
                        PreviousWin = i.PreviousWin,
                        FollowingWin = i.FollowingWin
                    }).ToList();

                return Ok(new IntervalResponse
                {
                    Min = minResults,
                    Max = maxResults
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
