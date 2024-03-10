// ChildComponent.tsx
interface Props {
	id: string | string[] | undefined
}

const Test: React.FC<Props> = ({ id }) => {
	return (
		<div>
			<h1>ID: {id}</h1>
		</div>
	)
}

export default Test
