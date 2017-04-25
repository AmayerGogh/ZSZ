
using System;
/**
*@项目名称: 停车场岗亭管理软件系统
*@文件名称: ZSZStringModelBinder
*@Date: 2017/4/6 15:13:01
*@Copyright: 2015 悦畅科技有限公司. All rights reserved.
*注意：本内容仅限于悦畅科技有限公司内部传阅，禁止外泄以及用于其他的商业目的
*/
using System.Web.Mvc;

namespace ZSZ.Web.Common.Binder
{
    //注意命名空间：不用使用其他命名空间的同名类
    public class ZSZStringModelBinder: DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            string value = valueResult.AttemptedValue;
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            value = value.Trim();

            //自动转换类型
            return Convert.ChangeType(value, bindingContext.ModelType);
        }
    }
}
