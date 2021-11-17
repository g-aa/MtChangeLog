class AuthorEditWindow {
    constructor(editableObj){
        
        let _editableObj = editableObj;
        if(_editableObj == undefined || _editableObj == null){
            throw new Error("не выбран автор для работы");
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
                        label:"Автор проекта (редакции):"
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
            let lWidth = 120;
            let result = {
                view:"layout",
                id:"winLayout",
                rows:[
                    { 
                        view:"text", 
                        label:"Имя:", 
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editableObj.getFirstName(),
                        attributes:{
                            maxlength:32
                        }, 
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editableObj.setFirstName(newValue);    
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"text", 
                        label:"Фамилия:", 
                        labelAlign:"right",
                        labelWidth:lWidth,
                        value:_editableObj.getLastName(),
                        attributes:{
                            maxlength:32
                        }, 
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editableObj.setLastName(newValue);
                                } catch (error){
                                    messageBox.warning(error.message);
                                }
                            }
                        }
                    },
                    {
                        view:"text",
                        label:"Должность:",
                        labelAlign:"right",
                        labelWidth:lWidth, 
                        value:_editableObj.getPosition(),
                        attributes:{
                            maxlength:100
                        }, 
                        on:{
                            onChange: async function(newValue, oldValue, config){
                                try{
                                    _editableObj.setPosition(newValue);
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
                                await _editableObj.submit();
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