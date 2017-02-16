using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class UnitOfWork : IDisposable
    {
        private WebAPIEntities context;
        private GenericRepository<user> userRepository;
        private GenericRepository<token> tokenRepository;
        private GenericRepository<product> productRepository;

        public UnitOfWork()
        {
            context = new WebAPIEntities();
        }



        public GenericRepository<user> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<user>(context);
                }
                return this.userRepository;
            }
        }

        public GenericRepository<token> TokenRepository
        {
            get
            {
                if (this.tokenRepository == null)
                {
                    this.tokenRepository = new GenericRepository<token>(context);
                }
                return this.tokenRepository;
            }
        }
        public GenericRepository<product> ProductRepository
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new GenericRepository<product>(context);
                }
                return this.productRepository;
            }
        }


        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var outputLines = new List<string>();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state\"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name,
                    eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"",
                        ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);
                throw ex;
            }
        }


        #region Implements Dispose

        private bool dispose = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.dispose)
            {
                if (disposing)
                {
                    Console.WriteLine("UnitOfWork is disposed");
                    context.Dispose();
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
