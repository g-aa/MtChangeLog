class ProjectRevisionEditWindow {
    constructor(editablePrjRev) {

        /*
        let _editablePrjRev = editablePrjRev;
        if(_editablePrjRev == undefined || _editablePrjRev == null){
            throw new Error("не выбрана ревизия проекта для работы");
        }
        */

        let _uiWindow;
        this.show = function() {
            webix.ready(function () {
                _uiWindow = webix.ui(window());
                _uiWindow.show();
            });
        }

        let window = function () {
            let result = {
                view:"window",
                id:"win_id",
                modal:true,
                move:true,
                width:800,
                height:800,
		        position:"center",
                head:headLayout(),
                body:windowLayout()
            };
            return result;
        };

        let headLayout = function () {
            let result = {
                view:"toolbar",
                id:"headToolbar_id",
                height:40,
                elements:[
                    {
                        view:"label",
                        label:"Редакция БФПО:"
                    },
                    {
                        
                    },
                    {
                        view:"icon",
                        icon:"wxi-close",
                        id:"winCloseBtn_id",
                        align:"right",
                        tooltip:"закрыть окно",
                        click: function () {
                            _uiWindow.close();
                        }
                    }
                ]
            };
            return result;
        };

        let windowLayout = function () {
            let lWidth = 230; // label width
            let dHeight = 300; // description height
            let result = {
                view:"layout",
                id:"winLayout",
                rows:[
                    {
                        view:"combo",
                        id:"projectVersion_id",
                        label:"Версия БФПО:",
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:"",
                        options:[],
                        on:{
                            onChange: async function(newValue, oldValue, config){
                            }
                        }
                    },
                    {
                        view:"combo",
                        id:"parentRevision_id",
                        label:"Предыдущая редакция БФПО:",
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:"",
                        options:[],
                        on:{
                            onChange: async function(newValue, oldValue, config){
                            }
                        }
                    },
                    {
                        view:"combo",
                        id:"revisionArmEdit_id",
                        label:"Версия ArmEdit:",
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:"",
                        options:[],
                        on:{
                            onChange: async function(newValue, oldValue, config){
                            }
                        }
                    },
                    {
                        view:"text",
                        id:"revision_id", 
                        label:"Номер редакции:", 
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:"",
                        attributes:{
                            maxlength:2
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config){
                            }
                        }
                    },
                    {
                        view:"datepicker",
                        id:"revisionDate_id",
                        // inputWidth:300,
                        label:'Дата редакции:',
                        labelAlign:"right",
                        labelWidth:lWidth,
                        timepicker:false,
                        value:new Date(),
                        on:{
                            onChange: function(newValue, oldValue, config){
                            }
                        }
                    },
                    {
                        view:"combo",
                        id:"revisionAuthor_id",
                        label:"Автор редакции:",
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:"",
                        options:[],
                        on:{
                            onChange: async function(newValue, oldValue, config){
                            }
                        }
                    },
                    {
                        view:"text",
                        id:"revisionReason_id", 
                        label:"Причина изменений:", 
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:"",
                        attributes:{
                            maxlength:250
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config){
                            }
                        }
                    },
                    {
                        view:"textarea",
                        id:"revisionDescription_id",
                        label:"Описание:",
                        labelAlign:"right",
                        labelWidth:lWidth, 
                        height:dHeight, 
                        value:"",
                        attributes:{
                            maxlength:1000
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config) {
                            }
                        }
                    },
                    {
                        view:"button",
                        id:"submitButton_id",
                        value:"Отправить",
                        css:"webix_primary",
                        inputWidth:200,
                        align:"right",
                        click: async function (){
                            try{ 
                                // await _editablePrjRev.submit();
                            } catch (error){
                                messageBox.alertWarning(error.message);
                            } finally{
                                _uiWindow.close();
                            }
                        }
                    }
                ]
            };
            return result;
        };
    }
}