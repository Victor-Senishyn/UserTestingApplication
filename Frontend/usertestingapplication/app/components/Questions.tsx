import { useEffect, useState } from 'react'
import { getQuestions } from '../services/questions'

interface QuestionsProps {
	testId: number
}

export const Questions: React.FC<QuestionsProps> = ({ testId }) => {
	const [questions, setQuestions] = useState<Question[]>([])
	const [loading, setLoading] = useState(true)

	useEffect(() => {
		const fetchQuestions = async () => {
			setLoading(true)
			const fetchedQuestions = await getQuestions(testId)
			setQuestions(fetchedQuestions)
			setLoading(false)
		}

		fetchQuestions()
	}, [testId])

	if (loading) return <div>Loading...</div>

	return (
		<div>
			{questions.map((question, index) => (
				<div key={index}>
					<h3>Question {index + 1}</h3>
					<p>{question.text}</p>
					{/* Display other details of the question */}
				</div>
			))}
		</div>
	)
}
