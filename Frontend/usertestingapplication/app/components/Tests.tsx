import Card from "antd/es/card/Card"
import { CardTitle } from "./CardTitle"
import Button from 'antd/es/button/button'
import Link from "next/link"

interface Props {
    tests: Test[]
}

export const Tests = ({tests}: Props) => {
    return (
			<div className='cards'>
				{tests.map((test: Test) => (
					<Card key={test.id} title={<CardTitle title={test.title} />}>
						<p>{test.description}</p>
						<Link
							href={{ pathname: `/test`, query: { id: test.id } }}
						>
							<Button>Start test</Button>
						</Link>
					</Card>
				))}
			</div>
		)
}