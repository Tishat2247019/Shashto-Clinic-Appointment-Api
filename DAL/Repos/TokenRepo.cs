﻿using DAL.EF.Tables;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class TokenRepo : Repo, IRepo<Token, string, Token>
    {
        public Token Create(Token obj)
        {
            db.Tokens.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public Token Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Token> Get()
        {
            throw new NotImplementedException();
        }

        public Token Get(string key)
        {
            return db.Tokens.FirstOrDefault(t => t.Key == key);
        }

        public Token Update(Token obj)
        {
            var tk = Get(obj.Key);
            if (tk != null)
            {
                db.Entry(tk).CurrentValues.SetValues(obj);
                db.SaveChanges();
                return tk;
            }
            return null;
        }
    }
}
