﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopWebGisEntityFrameWorkCore.EntityFrameWorkCore;

namespace ShopWebGisEntityFrameWorkCore.Migrations
{
    [DbContext(typeof(ShopWebGisDbContext))]
    partial class ShopWebGisDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("MenuInfoRoleInfo", b =>
                {
                    b.Property<int>("MenusId")
                        .HasColumnType("int");

                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.HasKey("MenusId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("MenuInfoRoleInfo");
                });

            modelBuilder.Entity("RoleInfoUserInfo", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleInfoUserInfo");
                });

            modelBuilder.Entity("ShopWebGisDomain.Shop.GoodClassification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime")
                        .HasColumnName("createtime")
                        .HasComment("创建时间");

                    b.Property<string>("CreateUserId")
                        .HasColumnType("text")
                        .HasColumnName("createuserid")
                        .HasComment("创建用户Id");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text")
                        .HasColumnName("createusername")
                        .HasComment("创建用户名称");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("ParentId")
                        .HasColumnType("int")
                        .HasColumnName("parentId");

                    b.Property<int>("Sort")
                        .HasColumnType("int")
                        .HasColumnName("sort");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime")
                        .HasColumnName("updatetime")
                        .HasComment("修改时间");

                    b.Property<string>("UpdateUserId")
                        .HasColumnType("text")
                        .HasColumnName("updateuserid")
                        .HasComment("更新操作用户Id");

                    b.Property<string>("UpdateUserName")
                        .HasColumnType("text")
                        .HasColumnName("updateusername")
                        .HasComment("更新操作用户名称");

                    b.Property<bool>("isSoftDelete")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("issoftdelete")
                        .HasComment("是否删除");

                    b.HasKey("Id");

                    b.ToTable("goodClassification");
                });

            modelBuilder.Entity("ShopWebGisDomain.User.MenuInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime")
                        .HasColumnName("createtime")
                        .HasComment("创建时间");

                    b.Property<string>("CreateUserId")
                        .HasColumnType("text")
                        .HasColumnName("createuserid")
                        .HasComment("创建用户Id");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text")
                        .HasColumnName("createusername")
                        .HasComment("创建用户名称");

                    b.Property<string>("IconClass")
                        .HasColumnType("text")
                        .HasColumnName("icon")
                        .HasComment("菜单图标");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name")
                        .HasComment("菜单名");

                    b.Property<string>("ParentId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("parentId")
                        .HasComment("父菜单Id");

                    b.Property<int>("Sort")
                        .HasColumnType("int")
                        .HasColumnName("sort")
                        .HasComment("排序");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime")
                        .HasColumnName("updatetime")
                        .HasComment("修改时间");

                    b.Property<string>("UpdateUserId")
                        .HasColumnType("text")
                        .HasColumnName("updateuserid")
                        .HasComment("更新操作用户Id");

                    b.Property<string>("UpdateUserName")
                        .HasColumnType("text")
                        .HasColumnName("updateusername")
                        .HasComment("更新操作用户名称");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("url")
                        .HasComment("Url 对应前端路由");

                    b.Property<bool>("isSoftDelete")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("issoftdelete")
                        .HasComment("是否删除");

                    b.HasKey("Id");

                    b.ToTable("menu");

                    b
                        .HasComment("菜单");
                });

            modelBuilder.Entity("ShopWebGisDomain.User.RoleInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime")
                        .HasColumnName("createtime")
                        .HasComment("创建时间");

                    b.Property<string>("CreateUserId")
                        .HasColumnType("text")
                        .HasColumnName("createuserid")
                        .HasComment("创建用户Id");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text")
                        .HasColumnName("createusername")
                        .HasComment("创建用户名称");

                    b.Property<string>("RoleDescription")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("rolename");

                    b.Property<int>("RoleShot")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime")
                        .HasColumnName("updatetime")
                        .HasComment("修改时间");

                    b.Property<string>("UpdateUserId")
                        .HasColumnType("text")
                        .HasColumnName("updateuserid")
                        .HasComment("更新操作用户Id");

                    b.Property<string>("UpdateUserName")
                        .HasColumnType("text")
                        .HasColumnName("updateusername")
                        .HasComment("更新操作用户名称");

                    b.Property<bool>("isSoftDelete")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("issoftdelete")
                        .HasComment("是否删除");

                    b.HasKey("Id");

                    b.ToTable("role");
                });

            modelBuilder.Entity("ShopWebGisDomain.User.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime")
                        .HasColumnName("createtime")
                        .HasComment("创建时间");

                    b.Property<string>("CreateUserId")
                        .HasColumnType("text")
                        .HasColumnName("createuserid")
                        .HasComment("创建用户Id");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text")
                        .HasColumnName("createusername")
                        .HasComment("创建用户名称");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime")
                        .HasColumnName("updatetime")
                        .HasComment("修改时间");

                    b.Property<string>("UpdateUserId")
                        .HasColumnType("text")
                        .HasColumnName("updateuserid")
                        .HasComment("更新操作用户Id");

                    b.Property<string>("UpdateUserName")
                        .HasColumnType("text")
                        .HasColumnName("updateusername")
                        .HasComment("更新操作用户名称");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("username")
                        .HasComment("用户名");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("userpassword")
                        .HasComment("用户密码");

                    b.Property<string>("UserPhone")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("userphone")
                        .HasComment("用户号码");

                    b.Property<bool>("isSoftDelete")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("issoftdelete")
                        .HasComment("是否删除");

                    b.HasKey("Id");

                    b.ToTable("user");

                    b
                        .HasComment("用户");
                });

            modelBuilder.Entity("MenuInfoRoleInfo", b =>
                {
                    b.HasOne("ShopWebGisDomain.User.MenuInfo", null)
                        .WithMany()
                        .HasForeignKey("MenusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopWebGisDomain.User.RoleInfo", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoleInfoUserInfo", b =>
                {
                    b.HasOne("ShopWebGisDomain.User.RoleInfo", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopWebGisDomain.User.UserInfo", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShopWebGisDomain.User.UserInfo", b =>
                {
                    b.OwnsOne("ShopWebGisDomain.ValueObject.Address", "Address", b1 =>
                        {
                            b1.Property<int>("UserInfoId")
                                .HasColumnType("int");

                            b1.Property<string>("AddressDetail")
                                .HasMaxLength(200)
                                .HasColumnType("varchar(200)")
                                .HasColumnName("addressdetail")
                                .HasComment("详细地址");

                            b1.Property<string>("City")
                                .HasMaxLength(25)
                                .HasColumnType("varchar(25)")
                                .HasColumnName("city")
                                .HasComment("城市");

                            b1.Property<string>("County")
                                .HasMaxLength(25)
                                .HasColumnType("varchar(25)")
                                .HasColumnName("county")
                                .HasComment("区县镇");

                            b1.Property<string>("Province")
                                .HasMaxLength(25)
                                .HasColumnType("varchar(25)")
                                .HasColumnName("province")
                                .HasComment("省份");

                            b1.HasKey("UserInfoId");

                            b1.ToTable("user");

                            b1.WithOwner()
                                .HasForeignKey("UserInfoId");
                        });

                    b.Navigation("Address");
                });
#pragma warning restore 612, 618
        }
    }
}
