from dsmltf import Counter, defaultdict, partial
from math import log

def entropy(class_probabilities):
    return sum(-p*log(p,2) for p in class_probabilities if p)

def class_probabilities(labels):
    total_count = len(labels)
    return [count/total_count for count in Counter(labels).values()]

def data_entropy(labeled_data):
    labels = [label for _,label in labeled_data]
    probabilities = class_probabilities(labels)
    return entropy(probabilities)

def partition_entropy(subsets):
    total_count = sum(len(subset) for subset in subsets)
    return sum(data_entropy(subset) * len(subset) / total_count for subset in subsets)

def partition_by(inputs,attribute):
    groups = defaultdict(list)
    for inp in inputs:
        key = inp[0][attribute]
        groups[key].append(inp)
    return groups

def partition_entropy_by(inputs,attribute):
    partitions = partition_by(inputs,attribute)
    return partition_entropy(partitions.values())

def build_tree_id3(inputs, split_candidates=None):
    if split_candidates is None:
        split_candidates = inputs[0][0].keys()
    num_inputs = len(inputs)
    num_trues = len([label for item, label in inputs if label])
    num_falses = num_inputs - num_trues
    if num_trues == 0: return False
    if num_falses == 0: return True
    if not split_candidates:
        return num_trues >= num_falses
    best_attribute = min(split_candidates, key = partial(partition_entropy_by, inputs))
    partitions = partition_by(inputs, best_attribute)
    new_candidates = [a for a in split_candidates if a != best_attribute]
    subtrees = {attribute_value : build_tree_id3(subset, new_candidates) for attribute_value,subset in iter(partitions.items())}
    subtrees[None] = num_trues > num_falses
    return (best_attribute, subtrees)

def classify(tree, inp):
    if tree in (True, False): return tree
    attribute, subtree_dict = tree
    subtree_key = inp.get(attribute)
    if subtree_key not in subtree_dict: subtree_key = None
    subtree = subtree_dict[subtree_key]
    return classify (subtree, inp)

def main():
    inputs_train = [
        ({'level': 'Senior', 'lang': 'Java', 'tweets': 'no', 'phd': 'no', 'experience': "yes", 'education': 'Bachelor',
          'certifications': 'yes', 'english_proficiency': 'advanced', 'location': 'office',
          'strengths': 'problem-solving', 'publications': 'no', 'recommendations': 'yes', 'extra_skills': 'AWS'},
         False),
        ({'level': 'Senior', 'lang': 'Java', 'tweets': 'no', 'phd': 'yes', 'experience': "yes", 'education': 'PhD',
          'certifications': 'yes', 'english_proficiency': 'advanced', 'location': 'office',
          'strengths': 'technical knowledge', 'publications': 'yes', 'recommendations': 'no', 'extra_skills': 'Docker'},
         True),
        ({'level': 'Mid', 'lang': 'Python', 'tweets': 'no', 'phd': 'no', 'experience': "yes", 'education': 'Master',
          'certifications': 'no', 'english_proficiency': 'intermediate', 'location': 'remote', 'strengths': 'teamwork',
          'publications': 'no', 'recommendations': 'yes', 'extra_skills': 'NoSQL'}, True),
        ({'level': 'Junior', 'lang': 'Python', 'tweets': 'no', 'phd': 'no', 'experience': "yes", 'education': 'Bachelor',
          'certifications': 'no', 'english_proficiency': 'basic', 'location': 'remote', 'strengths': 'problem-solving',
          'publications': 'no', 'recommendations': 'no', 'extra_skills': 'Docker'}, True),
        ({'level': 'Junior', 'lang': 'Python', 'tweets': 'no', 'phd': 'yes', 'experience': "yes", 'education': 'Master',
          'certifications': 'no', 'english_proficiency': 'basic', 'location': 'office',
          'strengths': 'technical knowledge', 'publications': 'yes', 'recommendations': 'no',
          'extra_skills': 'Kubernetes'}, False),
        ({'level': 'Senior', 'lang': 'Java', 'tweets': 'no', 'phd': 'no', 'experience': "yes", 'education': 'Bachelor',
          'certifications': 'yes', 'english_proficiency': 'advanced', 'location': 'office',
          'strengths': 'problem-solving', 'publications': 'no', 'recommendations': 'yes', 'extra_skills': 'AWS'}, True),
        ({'level': 'Mid', 'lang': 'Python', 'tweets': 'yes', 'phd': 'no', 'experience': "yes", 'education': 'Master',
          'certifications': 'no', 'english_proficiency': 'intermediate', 'location': 'remote', 'strengths': 'teamwork',
          'publications': 'no', 'recommendations': 'yes', 'extra_skills': 'NoSQL'}, True),
        ({'level': 'Junior', 'lang': 'Python', 'tweets': 'no', 'phd': 'no', 'experience': "yes", 'education': 'Bachelor',
          'certifications': 'no', 'english_proficiency': 'basic', 'location': 'remote', 'strengths': 'problem-solving',
          'publications': 'no', 'recommendations': 'no', 'extra_skills': 'Docker'}, False),
        ({'level': 'Mid', 'lang': 'C++', 'tweets': 'no', 'phd': 'no', 'experience': "yes", 'education': 'Bachelor',
          'certifications': 'yes', 'english_proficiency': 'advanced', 'location': 'office',
          'strengths': 'technical knowledge', 'publications': 'no', 'recommendations': 'yes', 'extra_skills': 'AWS'},
         True),
        ({'level': 'Senior', 'lang': 'JavaScript', 'tweets': 'no', 'phd': 'yes', 'experience': "yes", 'education': 'PhD',
          'certifications': 'yes', 'english_proficiency': 'advanced', 'location': 'remote',
          'strengths': 'problem-solving', 'publications': 'yes', 'recommendations': 'yes', 'extra_skills': 'Docker'},
         True),
        ({'level': 'Junior', 'lang': 'Java', 'tweets': 'yes', 'phd': 'yes', 'experience': "no", 'education': 'Master',
          'certifications': 'no', 'english_proficiency': 'basic', 'location': 'office',
          'strengths': 'technical knowledge', 'publications': 'no', 'recommendations': 'no', 'extra_skills': 'NoSQL'},
         False),
        ({'level': 'Mid', 'lang': 'Python', 'tweets': 'no', 'phd': 'no', 'experience': "yes", 'education': 'Bachelor',
          'certifications': 'yes', 'english_proficiency': 'intermediate', 'location': 'remote', 'strengths': 'teamwork',
          'publications': 'yes', 'recommendations': 'yes', 'extra_skills': 'Kubernetes'}, True),
        ({'level': 'Senior', 'lang': 'JavaScript', 'tweets': 'yes', 'phd': 'no', 'experience': "yes",
          'education': 'Bachelor', 'certifications': 'no', 'english_proficiency': 'advanced', 'location': 'office',
          'strengths': 'technical knowledge', 'publications': 'no', 'recommendations': 'no', 'extra_skills': 'Docker'},
         False),
        ({'level': 'Junior', 'lang': 'Python', 'tweets': 'no', 'phd': 'no', 'experience': "yes", 'education': 'Bachelor',
          'certifications': 'yes', 'english_proficiency': 'intermediate', 'location': 'remote',
          'strengths': 'problem-solving', 'publications': 'no', 'recommendations': 'no', 'extra_skills': 'AWS'}, True),
        ({'level': 'Mid', 'lang': 'Java', 'tweets': 'no', 'phd': 'no', 'experience': "yes", 'education': 'Bachelor',
          'certifications': 'no', 'english_proficiency': 'basic', 'location': 'office', 'strengths': 'teamwork',
          'publications': 'no', 'recommendations': 'no', 'extra_skills': 'Kubernetes'}, False)
    ]

    input_test =[
    ({'level': 'Junior', 'lang': 'Python', 'tweets': 'yes', 'phd': 'no', 'experience': "no", 'education': 'Bachelor', 'certifications': 'no', 'english_proficiency': 'basic', 'location': 'office', 'strengths': 'teamwork', 'publications': 'no', 'recommendations': 'no', 'extra_skills': 'NoSQL'}, False),
    ({'level': 'Mid', 'lang': 'JavaScript', 'tweets': 'no', 'phd': 'no', 'experience': "yes", 'education': 'Master', 'certifications': 'yes', 'english_proficiency': 'intermediate', 'location': 'office', 'strengths': 'technical knowledge', 'publications': 'no', 'recommendations': 'yes', 'extra_skills': 'AWS'}, True),
    ({'level': 'Senior', 'lang': 'C++', 'tweets': 'no', 'phd': 'yes', 'experience': "yes", 'education': 'PhD', 'certifications': 'yes', 'english_proficiency': 'advanced', 'location': 'remote', 'strengths': 'problem-solving', 'publications': 'yes', 'recommendations': 'yes', 'extra_skills': 'Kubernetes'}, True),
    ({'level': 'Mid', 'lang': 'Python', 'tweets': 'yes', 'phd': 'no', 'experience': "yes", 'education': 'Bachelor', 'certifications': 'no', 'english_proficiency': 'basic', 'location': 'office', 'strengths': 'teamwork', 'publications': 'no', 'recommendations': 'no', 'extra_skills': 'NoSQL'}, False),
    ({'level': 'Senior', 'lang': 'Java', 'tweets': 'no', 'phd': 'no', 'experience': "yes", 'education': 'Master', 'certifications': 'yes', 'english_proficiency': 'advanced', 'location': 'office', 'strengths': 'technical knowledge', 'publications': 'no', 'recommendations': 'yes', 'extra_skills': 'Docker'}, True),
    ({'level': 'Junior', 'lang': 'JavaScript', 'tweets': 'no', 'phd': 'no', 'experience': "no", 'education': 'Bachelor', 'certifications': 'yes', 'english_proficiency': 'intermediate', 'location': 'office', 'strengths': 'problem-solving', 'publications': 'no', 'recommendations': 'no', 'extra_skills': 'AWS'}, True)
]


    tree = build_tree_id3(inputs_train)

    counter = 0
    for label in input_test:
        result = classify(tree, label[0])
        print(result)
        if result == label[1]:
            counter += 1
    print(f"Из {len(input_test)} правильно определено {counter}")

if __name__ == '__main__':
    main()