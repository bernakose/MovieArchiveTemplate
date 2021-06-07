using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieArchiveTemplate.Models.ViewModels
{
    public enum SuccessState
    {
        NotSucces,
        Success
    }

    public enum MemberType
    {
        Admin,
        User
    }


    public enum Country
    {
        tr,
        en
    }
}