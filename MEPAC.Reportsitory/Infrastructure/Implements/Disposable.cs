﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEPAC.Reportsitory.Infrastructure.Implements
{
    public class Disposable : IDisposable
    {
        private bool isDisposable;

        ~Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!isDisposable && disposing)
            {
                DisposeCore();
            }

            isDisposable = true;
        }

        // Override this to dispose custom object
        protected virtual void DisposeCore()
        {
        }
    }
}
