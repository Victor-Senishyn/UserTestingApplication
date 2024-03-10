'use client'

import { useState } from 'react'
import { signIn, signUp } from './services/auth'
import { Button, Form, Input } from 'antd'
import styles from './page.module.css'

export default function Home() {
	const [form] = Form.useForm()
	const [isSignIn, setIsSignIn] = useState(true)

	const onFinish = async (values: { email: string; password: string }) => {
		if (isSignIn) {
			await signIn(values.email, values.password)
		} else {
			await signUp(values.email, values.password)
		}
		form.resetFields()
	}

	return (
		<div className={styles.container}>
			<Form form={form} onFinish={onFinish} className={styles.form}>
				<Form.Item
					name='email'
					rules={[{ required: true, message: 'Please input your email!' }]}
				>
					<Input placeholder='Email' />
				</Form.Item>
				<Form.Item
					name='password'
					rules={[{ required: true, message: 'Please input your password!' }]}
				>
					<Input.Password placeholder='Password' />
				</Form.Item>
				<Form.Item>
					<Button htmlType='submit'>
						{isSignIn ? 'Sign In' : 'Sign Up'}
					</Button>
				</Form.Item>
			</Form>
			<div>
				{isSignIn ? (
					<p>
						Don't have an account?{' '}
						<Button type='link' onClick={() => setIsSignIn(false)}>
							Sign Up
						</Button>
					</p>
				) : (
					<p>
						Already have an account?{' '}
						<Button type='link' onClick={() => setIsSignIn(true)}>
							Sign In
						</Button>
					</p>
				)}
			</div>
		</div>
	)
}
