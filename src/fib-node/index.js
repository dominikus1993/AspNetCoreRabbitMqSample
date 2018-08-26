'use strict';

const express = require('express');
const amqp = require('amqplib/callback_api');
// Constants
const PORT = process.env.DOCKER_PORT || 3000;
const HOST = '0.0.0.0';
const RABBIT_CONNECTION = process.env.RABBIT_CONNECTION || "amqp://localhost:5672";
// App
const app = express();
app.get('/', (req, res) => {
    res.send('Hello world\n');
});

function fib(n) {
    let x = 0, y = 1, z = 1;
    for (let i = 0; i < n; i++) {
        x = y;
        y = z;
        z = x + y;
    }
    return x;
}

amqp.connect(RABBIT_CONNECTION, (err, conn) => {
    console.log("Connected")
    conn.createChannel((err, ch) => {
    if (err) {
        console.error(err);
    }

    const exchange = "Fib.Common.Messages.FibCalculated, Fib.Common"
    ch.assertExchange(exchange, "topic")
    const q = "Test222"
    ch.assertQueue(q)
    ch.bindQueue(q, exchange, "")
    ch.consume(q, (msg) => {
        console.log("Elo")
    const message = JSON.parse(msg.content.toString())
    console.log(message);
}, {noAck: true})
// ch.assertQueue("fib", { durable: false }, (err, q) => {
//   if (err) {
//     console.error(err);
//   }
//   console.log(" [*] Waiting for messages in %s. To exit press CTRL+C", q.queue);
//   ch.bindQueue(q.queue, "fib", '');
//   ch.consume(q.queue, (msg) => {
//     console.log(msg.content.toString());
//   })
// })
})
});

app.listen(PORT, HOST);
console.log(`Running on http://${HOST}:${PORT}`);