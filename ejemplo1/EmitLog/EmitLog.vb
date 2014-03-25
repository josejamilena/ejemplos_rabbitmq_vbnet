Imports System
Imports RabbitMQ.Client
Imports System.Text

Module EmitLog

    Sub Main(ByVal args() As String)
        Dim factory As ConnectionFactory = New ConnectionFactory()
        factory.HostName = "localhost"
        Dim connection = factory.CreateConnection()
        Dim channel = connection.CreateModel()
        channel.ExchangeDeclare("logs", "fanout")
        Dim message = GetMessage(args)
        Dim body = Encoding.UTF8.GetBytes(message)
        channel.BasicPublish("logs", "", Nothing, body)
        Console.WriteLine(" [x] Sent {0}", message)
    End Sub

    Private Function GetMessage(args As String()) As String
        If (args.Length > 0) Then
            Return String.Join(" ", args)
        Else
            Return "info: Hello World!"
        End If
    End Function

End Module
