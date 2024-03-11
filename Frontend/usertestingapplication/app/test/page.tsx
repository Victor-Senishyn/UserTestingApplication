'use client'
import React, { useEffect, useState } from 'react'
import { Tests } from '../components/Tests'
import { Button } from 'antd'
import { getAvailableTests, getCompletedTests } from '../services/tests'
import { getQuestions } from '../services/questions'

const Test = () => {
	
	const [tests, setTests] = useState<Test[]>([])

	useEffect(() => {
		const urlParams = new URLSearchParams(window.location.search)
		const idParam = urlParams.get('id')
		const getQuest = async () => {
			const tests = await getQuestions(Number(idParam))
			setTests(tests)
		}
		getQuest()
	}, [])

	return (
		<div>
			<Tests tests={tests} />
		</div>
	)
}

export default Test
