function getDataAndLinksForTree(projectData){
    if (typeof projectData == undefined){
        throw new Error('Нет данных о версиях и редакциях БМРЗ!');
    }
    
    let startPoint = { x:20, y:20 };
    let dPoint = { x:20, y:20 };
	let blokSize = { width:200, height:150 };
			
    //
    let projectTypes = [];  // для определения числа столбцов
    let projectDates = []; // для определения числа строк
    projectData.forEach(function(item, index, array){
        let temp = item.module + "-" + item.title + "-" + item.version;
        
        if(!projectTypes.includes(temp)){
            projectTypes.push(temp);
        }

        var d = Date.parse(item.date);
        if(!projectDates.includes(d)){
            projectDates.push(d);
        }
    });	
    projectTypes.sort();
    projectDates.sort();
							
    // формирование данных для графа визуализации
    let result = {
        data:[],
        links:[]
    };
    projectData.forEach(function(item, index, array){
        let value = "";
        
        // project full name:
        if(item.module != null){
            value += "ПрО: " + item.module;
        }
        if(item.title != null){
            value += "-" + item.title;
        }
        if(item.version != null){
            value += "-" + item.version;
        }
        if(item.revision != null){
            value += "_" + item.revision;
        }

        // compile date:
        if(item.date != null){
            value += "<br>От: " + item.date;
        }

        // armEdit version:
        if(item.armEdit != null){
            value += "<br><br>ArmEdit: " + item.armEdit;
        }

        if(item.platform != null){
            value += "<br>Платформа: " + item.platform;
        }

        let vIdx = projectTypes.indexOf((item.module + "-" + item.title + "-" + item.version));
        let dIdx = projectDates.indexOf(Date.parse(item.date));

        result.data.push({
            id:item.id,
            value:value,
            width:blokSize.width,
            height:blokSize.height,
            x:startPoint.x + vIdx*(blokSize.width + dPoint.x),
            y:startPoint.y + dIdx*(blokSize.height + dPoint.y),
        });
        
        //if(index > 0){
            result.links.push({
                source:item.parentId, 
                target:item.id,
                arrow:"triangle",
                lineWidth:2, 
                lineColor:"#0000FF",
                backgroundColor:"#0000FF"
            });
        //}
    });
    return result;
}