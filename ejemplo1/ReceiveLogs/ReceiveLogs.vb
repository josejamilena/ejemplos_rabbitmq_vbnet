Imports System
Imports RabbitMQ.Client
Imports RabbitMQ.Client.Events
Imports System.Text

Module ReceiveLogs

    Sub Main()
        Dim factory As ConnectionFactory = New ConnectionFactory()
        factory.HostName = "localhost"
        Dim connection = factory.CreateConnection()
        Dim channel = connection.CreateModel()
        channel.ExchangeDeclare("logs", "fanout")
        Dim queueName = channel.QueueDeclare()
        channel.QueueBind(queueName, "logs", "")
        Dim consumer = New QueueingBasicConsumer(channel)
        channel.BasicConsume(queueName, True, consumer)
        Console.WriteLine(" [*] Waiting for logs." & "To exit press CTRL+C")
        While (True)
            Dim ea As BasicDeliverEventArgs = consumer.Queue.Dequeue()
            Dim body = ea.Body
            Dim message = Encoding.UTF8.GetString(body)
            Console.WriteLine(" [x] {0}", message)
        End While

    End Sub

End Module
