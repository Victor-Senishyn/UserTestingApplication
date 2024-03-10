'use client'

import Button from 'antd/es/button/button'
import { useEffect, useState } from 'react'
import {
	getAvailableTests,
	getCompletedTests,
} from '../services/tests'
import { Tests } from '../components/Tests'

export default function TestsPage() {
	const [tests, setTests] = useState<Test[]>([])
	const [loading, setLoading] = useState(true)

	useEffect(() => {
		const getTests = async () => {
			const tests = await getAvailableTests()
			setLoading(false)
			setTests(tests)
		}
		getTests()
	}, [])

	const handleAvailableTestsClick = async () => {
		setLoading(true)
		const availableTests = await getAvailableTests()
		setLoading(false)
		setTests(availableTests)
	}

	const handleCompletedTestsClick = async () => {
		setLoading(true)
		const completedTests = await getCompletedTests()
		setLoading(false)
		setTests(completedTests)
	}

	return (
		<div>
			<Button onClick={handleAvailableTestsClick}>Available tests</Button>
			<Button onClick={handleCompletedTestsClick}>Completed tests</Button>
			<Tests tests={tests} />
		</div>
	)
}
