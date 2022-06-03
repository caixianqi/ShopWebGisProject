#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat
#父镜像，或者说是运行环境
FROM mcr.microsoft.com/dotnet/aspnet:3.1
#设置容器工作目录
WORKDIR /DockerDemo
#把当前目录的所有文件copy到工作目录中
COPY . /DockerDemo
#暴露一个端口让外部可以访问 要同上面设置的端口
EXPOSE 5000
ENTRYPOINT ["dotnet", "ShopWebGis.HttApi.Host.dll"]