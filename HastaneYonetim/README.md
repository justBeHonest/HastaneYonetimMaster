
![Dashboard](https://github.com/justBeHonest/Hastane2-master/blob/master/Hastane2-master/HastaneYonetim/HastaneYonetim/Content/images/anasayfa.JPG)

# Hastane Yönetim Sistemi

Hastane yönetim sisteminde belirtildiği gibi doktor ve yönetici rolleri bulunmaktadır. Doktor sadece kendi profilini ve randevularını görüntüleyebiliyor iken yönetici doktor ekleme işlemlerini, aktif ve pasif olma durumunu, tüm hastaları ve  randevularını, tüm kullanıcıları, günlük randevuları, yapılan bakım müdahalelerini, günlük randevular, tüm randevular ve yapılan bakım müdahalelelerinin raporlamaları için yazıcı, excel ve kopyalama seçeneği sunularak oluşturulması gibi ayrıcalıkları vardır. Sistem yönetici tarafından yönetiliyor ve oluşturulan doktor oturumun kullanılmasıyla sistemi doktorlar da ihtiyaçları doğrultusunda görüntüleyebiliyorlar. 

# Frameworks - kütüphaneler

1. ASP.NET MVC (version 5)
2. Entity Framework
3. Ninject
4. Automapper

# Projeyi Çalıştırma

- Visual Studio'yu açın
- web.config de connectionString'inizi ekleyin.
  ```
  <connectionString><add name="HastaneDB" connectionString="data source=Veritabanı Kaynağınız; initial catalog=HastaneDB;Integrated Security=True" providerName="System.Data.SqlClient" /></connectionString>
  ```
- NuGet Paket yöneticisi konsoluna aşağıdaki kodları yazın.
    ```
    - enable-migrations
    -  add-migration "InitialDb"
    -  update-database
   ```
- MSSQL Veritabanını açın
- AspNetRoles tablosuna Admin ve Doktor isminde iki rol ekleyin.
- Projeyi şu adreste çalıştırarak  http://localhost:xxxx/Hesap/Kayit ile yönetici ekleyebilirsiniz
