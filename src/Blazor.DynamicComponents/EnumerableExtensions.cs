#region Copyright & License

// Copyright © 2020 - 2023 Emmanuel Benitez
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

namespace BigSolution.Blazor;

/// <summary>
/// Provides extensions for <see cref="IEnumerable{T}"/>.
/// </summary>
public static class EnumerableExtensions
{
	/// <summary>Returns the input typed as <see cref="Dictionary{TKey,TValue}" />.</summary>
	/// <typeparam name="TKey">The type of dictionary key.</typeparam>
	/// <typeparam name="TValue">The type of dictionary value.</typeparam>
	/// <param name="source">The sequence to type as <see cref="IEnumerable{T}"/>.</param>
	/// <returns>The input sequence typed as <see cref="Dictionary{TKey,TValue}"/>.</returns>
	public static Dictionary<TKey, TValue> AsDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>>? source) 
		where TKey : notnull
	{
		return new Dictionary<TKey, TValue>(source ?? Enumerable.Empty<KeyValuePair<TKey, TValue>>());
	}
}
