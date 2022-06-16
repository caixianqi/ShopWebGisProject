/************************************************************************************

 * Copyright (c) 2022 All Rights Reserved.

 * CLR版本： 4.0.30319.42000

 *机器名称：LAPTOP-CC5K5UTK

 *命名空间：ShopWebGisDomainShare.Extension

 *文件名：  ConsulMiddleware

 *版本号：  V1.0.0.0

 *当前的用户域：LAPTOP-CC5K5UTK

 *创建人：  智慧环保部-蔡显麒

 *创建时间：2022/5/6 22:32:18

 *描述：Consul 中间件

/************************************************************************************/

using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebGisDomainShare.Extension
{
    public static class ConsulMiddleware
    {
        public static void ServiceRegister(this IApplicationBuilder app, IHostApplicationLifetime applicationLifetime, IConfiguration configuration)
        {

            
            string ip = configuration["Service:Address"];
            string port = configuration["Service:Port"];
            string serviceName = configuration["Service:Name"];
            string serviceId = Guid.NewGuid() + serviceName;

            string consulAddress = configuration["Consul:Ip"];
            string consulPort = configuration["Consul:Port"];
            var consulClient = new ConsulClient(x => x.Address = new Uri($"http://{consulAddress}:{consulPort}"));//请求注册的 Consul 地址
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                HTTP = $"http://{ip}:{port}/api/Health",//健康检查地址
                Timeout = TimeSpan.FromSeconds(5)
            };
            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = Guid.NewGuid().ToString(),
                Name = serviceName,
                Address = ip,
                Port = int.Parse(port),
                Tags = new[] { $"urlprefix-/{serviceName}" }//添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
            };

            consulClient.Agent.ServiceRegister(registration).Wait();//服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
            applicationLifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
            });
        }

    }
}
