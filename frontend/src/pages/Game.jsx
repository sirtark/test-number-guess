import React, { useState } from 'react'
import api from '../api'

const Game = () => {
    const [gameMessage, setGameMessage] = useState('')
    const [numberTry, setNumberTry] = useState('')
    const [gameStarted, setGameStarted] = useState(false)
    const sessionToken = localStorage.getItem('sessionToken')

    const handleStartGame = async () => {
        try {
            const response = await api.post('/start-game', {}, {
                headers: {
                    'sessionToken': sessionToken
                }
            })
            setGameStarted(true)
            console.log(response)
            setGameMessage(response.data)
        } catch (err) {
            setGameMessage('Error on game starting: ' + err.message)
        }
    }

    const handleGameTry = async () => {
        try {
            const response = await api.post(`/game-try?tryNumber=${Number(numberTry)}`, null, {
                headers: {
                    'sessionToken': sessionToken
                }
            });
            setGameMessage(response.data);
        } catch (err) {
            setGameMessage('error trying to send answer: ' + err.message);
        }
    };

    return (
        <div style={{ textAlign: 'center', marginTop: '50px' }}>
            <h1>Guess the number between 1 and 100</h1>
            {!gameStarted ? (
                <button onClick={handleStartGame}>Start Game</button>
            ) : (
                <div>
                    <input
                        type="number"
                        value={numberTry}
                        onChange={(e) => setNumberTry(e.target.value)}
                        placeholder="set a number"
                    />
                    <button onClick={handleGameTry}>Try</button>
                </div>
            )}
            {gameMessage && <p>{gameMessage}</p>}
        </div>
    )
}

export default Game