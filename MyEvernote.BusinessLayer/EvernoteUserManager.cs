using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using MyEvernote.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class EvernoteUserManager
    {
        private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();
        public BusinessLayerResult<EvernoteUser> RegisterUser(RegisterViewModel data)
        {
            // Kullanıcı username kontrolü
            // Kullanıcı e-posta kontrolü
            // Kayıt İşlemi
            // Aktivasyon e-postası gönderimi

            EvernoteUser user = repo_user.Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();
            if (user !=null)
            {
                if(user.Username == data.Username)
                {
                    layerResult.Errors.Add("Kullanıcı Adı Kayıtlı.");
                }

                if(user.Email == data.Email)
                {
                    layerResult.Errors.Add("E-mail Adresi Kayıtlı.");
                }
            }
            else
            {
                int dbResult = repo_user.Insert(new EvernoteUser()
                {
                    Username = data.Username,
                    Password = data.Password,
                    Email = data.Email,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false,
                  });

                if (dbResult > 0)
                {
                    layerResult.Result = repo_user.Find(x => x.Email == data.Email && x.Username == data.Username);
                    // TODO : aktivasyon  mail'ı atılacak...
                    //layerResult.Result.ActivateGuid
                }
            }

            return layerResult;
        }

        public BusinessLayerResult<EvernoteUser> LoginUser(LoginViewModel data)
        {
            // Giriş kontrolü
            // Hesap aktive edilmiş mi ? 


            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();
            layerResult.Result = repo_user.Find(x => x.Username == data.Username && x.Password == data.Password);
          
            if (layerResult.Result != null)
            {
                if (!layerResult.Result.IsActive)
                {
                    layerResult.Errors.Add("Kullanıcı aktifleştirilmemiştir. Lütfen e-posta adresinizi kontrol ediniz.");
                }
            }
            else
            {
                layerResult.Errors.Add("Kullanıcı adı yada şifre uyuşmuyor.");
            }
            return layerResult;
        }
    }
}
