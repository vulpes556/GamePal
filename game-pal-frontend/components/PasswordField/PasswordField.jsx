'use client';
import { useState } from 'react';
import { RiEyeFill } from "react-icons/ri";
import { RiEyeOffFill } from "react-icons/ri";

export default function PasswordField({ id, name }) {
    const [visible, setVisible] = useState(false);

    return (
        <div className='pw-field-main' >
            <input
                id={id}
                name={name}
                className='input'
                type={visible ? 'text' : 'password'}
            />
            <span
                className='icon'
                onClick={() => setVisible((prev) => !prev)}
                role="button"
            >
                {visible ? <RiEyeFill /> : <RiEyeOffFill />}
            </span>
        </div>
    );
}
