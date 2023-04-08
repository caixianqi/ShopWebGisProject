/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisXxlJob.Jobs

 *文件名：  CrawlerJDGoodsClass

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  蔡显麒

 *创建时间：2022/7/17 13:42:50

 *描述：爬虫-京东官网商品分类

/************************************************************************************/

using AutoMapper;
using DotXxlJob.Core;
using HtmlAgilityPack;
using IRepository;
using IRepository.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShopWebGisApplicationContract.Shop.Dto;
using ShopWebGisDomain.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebGisXxlJob.Jobs
{
    [JobHandler("CrawlerJDGoodsClassJob")]
    public class CrawlerJDGoodsClassJob : Job
    {
        private readonly ILogger<CrawlerJDGoodsClassJob> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IRepository<int, GoodClassification> _repository;
        private readonly IMapper _mapper;
        public CrawlerJDGoodsClassJob(ILogger<CrawlerJDGoodsClassJob> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration, IUnitOfWork iUnitOfWork, IMapper mapper)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _repository = iUnitOfWork.Repositorys<int, GoodClassification>();
            _mapper = mapper;
        }
        public async override Task<Rtd> Execute(JobContext jobExecuteContext)
        {
            IList<GoodClassification> goods = new List<GoodClassification>();
            int index = 1;
            log("开始爬虫任务");
            try
            {
                var url = _configuration["Crawler:JDUrl"];
                var client = _httpClientFactory.CreateClient(url);
                client.Timeout = TimeSpan.FromSeconds(30);
                var response = await client.GetStringAsync(url);
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(response);
                HtmlNode Node = htmlDocument.GetElementbyId("J_cate");// 获取Id
                var ulDocument = Node.SelectSingleNode("ul[@class='JS_navCtn cate_menu']");
                var liDocument = ulDocument.SelectNodes("li");
                if (liDocument.Any())
                {
                    foreach (var li in liDocument)
                    {
                        var aDocument = li.SelectNodes("a");
                        if (aDocument.Any())
                        {
                            foreach (var a in aDocument)
                            {
                                var goodName = a.InnerText;
                                GoodClassificationDto goodClassificationDto = new GoodClassificationDto();
                                goodClassificationDto.Name = goodName;
                                goodClassificationDto.Sort = index;
                                index++;
                                var good = _mapper.Map<GoodClassificationDto, GoodClassification>(goodClassificationDto);
                                goods.Add(good);
                            }
                        }
                    }
                }
                if (goods.Any())
                {
                    await _repository.InsertRangeAsync(goods);
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            log("结束爬虫任务");
            return Rtd.Success("任务完成");
        }
    }
}
