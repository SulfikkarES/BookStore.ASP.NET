﻿using EazyLearn.BookStore.Components;
using EazyLern.BookStore.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EazyLern.BookStore.Admin
{
    public partial class BookDetailsEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Fill();
            }
            btnSubmit.ServerClick += new EventHandler(BtnSubmit_Click);
            btnCancel.ServerClick += new EventHandler(BtnCancel_Click);
            ddlSpecialPriceStatus.SelectedIndexChanged += new EventHandler(SpecialPriceStatusChanged_Click);
        }

        void SpecialPriceStatusChanged_Click(object sender, EventArgs e)
        {
            if (ddlSpecialPriceStatus.SelectedValue == "No")
            {
                txtSpecialPrice.Value = "0.00";
            }
            else
            {
                txtValidation.InnerText = "Enter special price";
            }
        }

        void Fill()
        {
            int bookId = Convert.ToInt32(Request.QueryString["bookId"].ToString());

            Book objBook = new Book();
            DataTable dt = objBook.GetAllBookDetailsById(bookId);
            txtTitle.Value = dt.Rows[0]["Book Title"].ToString();
            txtAuthor.Value = dt.Rows[0]["Book Author"].ToString();
            txtPrice.Value = dt.Rows[0]["Book Price"].ToString();
            txtSpecialPrice.Value = dt.Rows[0]["Book Special Price"].ToString();
            txtDescription.Value = dt.Rows[0]["Book Description"].ToString();

            // Special Price Status drop down list initial values
            List<string> specialPriceStatus = new List<string> { "Yes", "No" };
            ddlSpecialPriceStatus.DataSource = specialPriceStatus;
            ddlSpecialPriceStatus.DataBind();

            //if (Convert.ToInt32(dt.Rows[0]["Special Price Status"]) == 1)
            //{
            //    ddlSpecialPriceStatus.SelectedValue = "Yes";
            //}
            //else
            //{
            //    ddlSpecialPriceStatus.SelectedValue = "No";
            //}

            Category objCat = new Category();
            ddlCategory.DataSource = objCat.GetAllCategories();
            ddlCategory.DataTextField = "cat_category_name";
            ddlCategory.DataValueField = "cat_category_id";
            ddlCategory.DataBind();

            ddlCategory.SelectedValue = dt.Rows[0]["Category Id"].ToString();
        }

        void BtnCancel_Click(object sender, EventArgs e)
        {
            Fill();
        }

        void BtnSubmit_Click(object sender, EventArgs e)
        {
            int rowsInserted = 0;

            if (Check(txtTitle.Value))
            {
                ShowMessage("*Enter title");
                return;
            }
            else if (Check(txtAuthor.Value))
            {
                ShowMessage("*Enter author name");
                return;
            }
            else if (Check(txtPrice.Value))
            {
                ShowMessage("*Enter price");
                return;
            }
            else if (Check(txtDescription.Value))
            {
                ShowMessage("*Enter Description");
                return;
            }
            else
            {
                Book objBook = new Book();
                objBook.Title = txtTitle.Value;
                objBook.Author = txtAuthor.Value;
                objBook.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                objBook.Price = Convert.ToDouble(txtPrice.Value);

                if (ddlSpecialPriceStatus.SelectedValue == "Yes")
                {
                    objBook.SpecialPriceStatus = 1;

                    if (Convert.ToDouble(txtSpecialPrice.Value) > objBook.Price)
                    {
                        ShowMessage("Special price cannot be greater than normal price");
                        return;
                    }
                    if (Convert.ToDouble(txtSpecialPrice.Value) == 0.0)
                    {
                        ShowMessage("Special price cannot be zero");
                        return;
                    }

                }
                else
                {
                    objBook.SpecialPriceStatus = 0;
                }

                objBook.SpecialPrice = Convert.ToDouble(txtSpecialPrice.Value);
                objBook.Description = txtDescription.Value;
                int bookId = Convert.ToInt32(Request.QueryString["bookId"].ToString());

                rowsInserted = objBook.UpdateBookDetails(bookId);
            }

            if (rowsInserted > 0)
            {
                ShowMessage("Successfully updated book details");
                Fill();
            }
            else
            {
                ShowMessage("*Data not updated");
            }

        }
        void ShowMessage(string msg)
        {
            txtValidation.InnerText = msg;
        }

        public bool Check(string str)
        {
            if (str == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
} 