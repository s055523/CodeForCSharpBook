//没有这段也行
//这是对mscorlib的引用，可以从其他地方直接抄过来
//.assembly extern mscorlib
//{
//    .ver 4:0:0:0
//    .publickeytoken = (B7 7A 5C 56 19 34 E0 89)
//}

.assembly helloWorld
{
}

//程序集的IL代码，所有的助记码都应该已经见过了
//其中，IL0001这些行号不是必须的，不影响程序运行
.method static void Main()
{
    .entrypoint
    .maxstack 1
 
    ldstr "Hello, world!"
    call void [mscorlib]System.Console::WriteLine(string)

    ret
}
