.assembly helloWorld
{
}

//方法不一定要使用Main命名
.method static void Add()
{
    .entrypoint
    .maxstack 2
	 //局部变量
    .locals init (int32 num1,
                  int32 num2)
				  
	//ldc.i4加载一个int32变量进栈，值为888
	ldc.i4 888
	
	//stloc将栈顶的变量弹出，并赋给num1（写成stloc.0也可，0对应num1）
	stloc num1 //stloc.0
	ldc.i4 999
    stloc num2 //stloc.1
	
	//将两个变量压入栈
	ldloc num1 //ldloc.0
	ldloc num2 //ldloc.1
	
	//add会将栈上最顶的两个数相加，然后弹出结果
	add
 
	//调用WriteLine的int32重载
    call void [mscorlib]System.Console::WriteLine(int32)

    ret
}
