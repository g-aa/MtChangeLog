class ArmEditWindow {
    constructor(editableObj){
        let _editableObj = editableObj;
        if(_editableObj == undefined || _editableObj == null){
            throw new Error("не выбран ArmEdit для работы");
        }

        let _uiWindow;
        this.show = function(){
            webix.ready(function (){
                _uiWindow = webix.ui(window());
                _uiWindow.show();
            });
        }

        let window = function (){
            let result = {
                view:"window",
                id:"win_id",
                modal:true,
                move:true,
                width:600,
                height:600,
		        position:"center",
                head:headLayout(),
                body:windowLayout()
            };
            return result;
        };
        
        let headLayout = function (){
            let result = {
                view:"toolbar",
                id:"headToolbar",
                height:40,
                elements:[
                    {
                        view:"label",
                        label:"ArmEdit:"
                    },
                    {
                        
                    },
                    {
                        view:"icon",
                        icon:"wxi-close",
                        id:"btn_winClose",
                        align:"right",
                        tooltip:"закрыть окно",
                        click: function (){
                            _uiWindow.close();
                        }
                    }
                ]
            };
            return result;
        };

        let windowLayout = function () {
            let lWidth = 160;
            let result = {
                view:"layout",
                id:"winLayout",
                rows:[
                    { 
                        view:"text", 
                        label:"Децимальный номер:", 
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editableObj.getDivg(),
                        attributes:{
                            maxlength:13
                        }, 
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editableObj.setDivg(newValue);    
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"text", 
                        label:"Версия:", 
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editableObj.getVersion(),
                        attributes:{
                            maxlength:11
                        }, 
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editableObj.setVersion(newValue);
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"datepicker",
                        label:'Дата релиза:',
                        labelAlign:"right",
                        labelWidth:lWidth,
                        timepicker:false,
                        format:"%Y-%m-%d",
                        value:_editableObj.getDate(),
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editableObj.setDate(newValue);
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"textarea",
                        label:"Описание:",
                        labelAlign:"right",
                        labelWidth:lWidth, 
                        height:120, 
                        value:_editableObj.getDescription(),
                        attributes:{
                            maxlength:255
                        }, 
                        on:{
                            onChange: function(newValue, oldValue, config){
                                try{
                                    _editableObj.setDescription(newValue);
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
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
                        click: async function(){
                            try{ 
                                // отправить:
                                await _editableObj.submit();

                                // автоматически закрывать при удачном стечении обстоятельств:
                                _uiWindow.close();
                            } catch (error){
                                messageBox.alertWarning(error.message);
                            } finally{
                            }
                        }
                    }
                ]
            };
            return result;
        };
    }
}