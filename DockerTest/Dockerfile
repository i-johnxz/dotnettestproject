FROM aspnetcore:2.1.libgdiplus AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.1 AS build

COPY . /src

WORKDIR /src/[projectpath]

RUN dotnet publish -c Release -o /app

FROM base AS final
RUN cp /usr/share/zoneinfo/Asia/Shanghai /etc/localtime && \ 
    echo 'Asia/Shanghai' >/etc/timezone

WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "[dllname].dll"]
