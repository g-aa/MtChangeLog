class MessageBox{
    constructor(){
        this.timeDelay = 10000; // milliseconds
    }

    information(sMessage = ""){
        webix.message({ text:"[INFO] - " + sMessage, type:"info", expire:this.timeDelay });
    }

    error(sMessage = ""){
        webix.message({ text:"[ERROR] - " + sMessage, type:"error", expire:this.timeDelay });
    }

    warning(sMessage = ""){
        webix.message({ text:"[ERROR] - " + sMessage, type:"warning", expire:this.timeDelay });
    }

    alertWarning(sMessage = ""){
        webix.modalbox({ title:"Warning!", type:"alert-warning", buttons:["Ok"], text:sMessage, width:650 });
    }
}

if(messageBox == undefined || messageBox == null){
    var messageBox = new MessageBox();
}