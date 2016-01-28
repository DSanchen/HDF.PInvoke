﻿/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Copyright by The HDF Group.                                               *
 * Copyright by the Board of Trustees of the University of Illinois.         *
 * All rights reserved.                                                      *
 *                                                                           *
 * This file is part of HDF5.  The full HDF5 copyright notice, including     *
 * terms governing use, modification, and redistribution, is contained in    *
 * the files COPYING and Copyright.html.  COPYING can be found at the root   *
 * of the source code distribution tree; Copyright.html can be found at the  *
 * root level of an installed copy of the electronic HDF5 document set and   *
 * is linked from the top-level documents page.  It can also be found at     *
 * http://hdfgroup.org/HDF5/doc/Copyright.html.  If you do not have          *
 * access to either file, you may request a copy from help@hdfgroup.org.     *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HDF.PInvoke;

using herr_t = System.Int32;
using hid_t = System.Int32;
using hsize_t = System.UInt64;

namespace UnitTests
{
    public partial class H5ATest
    {
        [TestMethod]
        public void H5Aiterate_by_nameTest1()
        {
            hid_t att = H5A.create(m_v2_test_file, "IEEE_F32BE",
                H5T.IEEE_F32BE, m_space_scalar);
            Assert.IsTrue(att >= 0);
            Assert.IsTrue(H5A.close(att) >= 0);

            att = H5A.create(m_v2_test_file, "IEEE_F64BE", H5T.IEEE_F64BE,
               m_space_scalar);
            Assert.IsTrue(att >= 0);
            Assert.IsTrue(H5A.close(att) >= 0);

            att = H5A.create(m_v2_test_file, "NATIVE_B8", H5T.NATIVE_B8,
               m_space_scalar);
            Assert.IsTrue(att >= 0);
            Assert.IsTrue(H5A.close(att) >= 0);

            ArrayList al = new ArrayList();
            hsize_t n = 0;
            // the callback is defined in H5ATest.cs
            H5A.operator_t cb = DelegateMethod;
            Assert.IsTrue(H5A.iterate_by_name(m_v2_test_file, ".",
                H5.index_t.INDEX_NAME, H5.iter_order_t.ITER_NATIVE, ref n, cb,
                al) >= 0);
            // we should have 3 elements in the array list
            Assert.IsTrue(al.Count == 3);

            att = H5A.create(m_v0_test_file, "IEEE_F32BE",
                H5T.IEEE_F32BE, m_space_scalar);
            Assert.IsTrue(att >= 0);
            Assert.IsTrue(H5A.close(att) >= 0);

            att = H5A.create(m_v0_test_file, "IEEE_F64BE", H5T.IEEE_F64BE,
               m_space_scalar);
            Assert.IsTrue(att >= 0);
            Assert.IsTrue(H5A.close(att) >= 0);

            att = H5A.create(m_v0_test_file, "NATIVE_B8", H5T.NATIVE_B8,
               m_space_scalar);
            Assert.IsTrue(att >= 0);
            Assert.IsTrue(H5A.close(att) >= 0);

            al.Clear();
            n = 0;
            Assert.IsTrue(H5A.iterate_by_name(m_v0_test_file, ".",
                H5.index_t.INDEX_NAME, H5.iter_order_t.ITER_NATIVE, ref n, cb,
                al) >= 0);
            // we should have 3 elements in the array list
            Assert.IsTrue(al.Count == 3);
        }

        [TestMethod]
        public void H5Aiterate_by_nameTest2()
        {
            ArrayList al = new ArrayList();
            hsize_t n = 0;
            // the callback is defined in H5ATest.cs
            H5A.operator_t cb = DelegateMethod;

            Assert.IsFalse(
                H5A.iterate_by_name(Utilities.RandomInvalidHandle(), ".",
                H5.index_t.INDEX_NAME, H5.iter_order_t.ITER_NATIVE, ref n,
                cb, al) >= 0);
        }
    }
}